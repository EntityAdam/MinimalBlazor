using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace MinimalBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<MyApp>("HIJEFF");
            await builder.Build().RunAsync();
        }
    }

    public class MyApp : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(1, "body");
            builder.AddMarkupContent(2, "<h1>Hello World!</h1> \r\n");
            builder.CloseElement();
            builder.OpenComponent<MyDynamicComponent>(3);
            builder.CloseComponent();
        }

        protected override Task OnInitializedAsync()
        {
            Console.WriteLine("Hello From MyApp!");
            return base.OnInitializedAsync();
        }
    }

    public class MyDynamicComponent : ComponentBase
    {
        [Inject]
        public IJSRuntime js { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(1, "input");
            builder.AddAttribute(2, "type", "text");
            builder.AddAttribute(3, "Value", Text);
            builder.CloseElement();

        }
        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("Hello From MyDynamicComponent!");
            await js.InvokeAsync<string>("window.alert", new string[] { "Hello World" });
        }

        [Parameter]
        public string Text { get; set; } = "Hello from parameter";
    }
}
