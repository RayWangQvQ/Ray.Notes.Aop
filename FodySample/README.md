# 静态代理-Fody

## 关于拦截器内的依赖注入

在 Rougamo.Fody 中，所有的 Mo 开头的 Attribute 类都是使用了 Fody 插件生成的。这些类是在编译时生成的，因此无法在运行时进行依赖注入。

如果你需要在这些 Attribute 类中使用依赖注入，你可以使用其他方式来实现。以下是一些可能的解决方案：

使用属性注入：你可以在你的 Attribute 类中定义一个属性，然后在使用该属性的地方，使用依赖注入将该属性设置为所需的值。例如：
public class MyAttribute : MoAttribute
{
    public MyAttribute()
    {
        // 此处不能使用依赖注入
    }

    public string MyProperty { get; set; } // 定义一个属性

    public override void OnEnter(MethodExecutionArgs args)
    {
        // 在这里使用属性
        Console.WriteLine(MyProperty);
    }
}

public class MyClass
{
    private readonly MyService _myService;

    public MyClass(MyService myService)
    {
        _myService = myService;
    }

    [My(MyProperty = "Hello, world!")]
    public void MyMethod()
    {
        // ...
    }
}
在上面的示例中，我们定义了一个 MyAttribute 类，并在其中定义了一个名为 MyProperty 的属性。然后，在 MyClass 中，我们使用 My Attribute 并设置了 MyProperty 的值为 "Hello, world!"。在 OnEnter 方法中，我们可以使用 MyProperty 属性的值。

使用服务定位器：你可以在你的 Attribute 类中使用服务定位器来获取所需的依赖项。例如：
public class MyAttribute : MoAttribute
{
    private readonly MyService _myService;

    public MyAttribute()
    {
        // 使用服务定位器获取 MyService 实例
        _myService = (MyService)ServiceLocator.Current.GetService(typeof(MyService));
    }

    public override void OnEnter(MethodExecutionArgs args)
    {
        // 在这里使用 MyService
        _myService.DoSomething();
    }
}
在上面的示例中，我们在 MyAttribute 中使用了服务定位器来获取 MyService 实例。在 OnEnter 方法中，我们可以使用 _myService 对象调用 DoSomething 方法。

请注意，服务定位器是一种反模式，因为它会导致代码变得难以测试和维护。因此，如果可能的话，你应该尽可能避免使用它。