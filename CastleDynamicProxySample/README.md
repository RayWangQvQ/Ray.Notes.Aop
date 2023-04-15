# 动态代理-Castle.Core

开源：[https://github.com/castleproject/Core](https://github.com/castleproject/Core)

以前叫是独立的`Castle.DynamicProxy.dll`，在2.5版本之后合并进了`Castle.Core.dll` 

## WithTarget和WithoutTarget的区别

WithOutTarget只针对接口做动态代理，也就是Interceptor里没有target对象，如果在里面调用invocation.Proceed()会异常，因此该模式需要在拦截器中实现自己的逻辑来处理方法调用。

## CreateInterfaceProxyWithTarget和CreateInterfaceProxyWithTargetInterface有什么区别

WithTarget要求传入target对象，动态代理内包裹的就是该target对象，即invocation.InvocationTarget拿到的，该对象的所有public的属性也都能拿到。

而WithTargetInterface要求传入的是接口，所以invocation.InvocationTarget拿到的是接口，是针对抽象的。

所以如果你的代码是规范的、依赖接口的，应优先考虑使用CreateInterfaceProxyWithTargetInterface。