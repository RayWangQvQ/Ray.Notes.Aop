# 1. Ray.Notes.Aop

<!-- TOC depthFrom:2 -->

- [1. What is AOP](#1-what-is-aop)
- [2. 一些名词](#2-一些名词)
    - [2.1. 目标对象（Target）](#21-目标对象target)
    - [2.2. 切面（Aspect）](#22-切面aspect)
    - [2.3. 织入（Weaving）](#23-织入weaving)
- [3. 实现AOP的几种原理](#3-实现aop的几种原理)
    - [3.1. 静态代理](#31-静态代理)
    - [3.2. 动态代理](#32-动态代理)
- [4. 实现](#4-实现)
    - [4.1. 静态代理-DIY](#41-静态代理-diy)
    - [4.2. 动态代理-DIY](#42-动态代理-diy)
    - [4.3. 静态代理-FOdy](#43-静态代理-fody)
    - [4.4. 动态代理-Castle DynamicProxy](#44-动态代理-castle-dynamicproxy)
    - [4.5. 动态代理-Autofac + Castle.DynamicProxy](#45-动态代理-autofac--castledynamicproxy)
- [5. Refers](#5-refers)

<!-- /TOC -->

## 1. What is AOP

Aspect Oriented Programming的缩写，意为：面向切面编程。

- 是对OOP的补充，是函数式编程的一种衍生范型
- 提高程序的可重用性，提高开发的效率
- 主要分为静态代理和动态代理
- AOP可以将日志记录，性能统计，安全控制，事务处理，异常处理等代码从业务逻辑代码中划分出来，通过对这些行为的分离，我们希望可以将它们独立到非指导业务逻辑的方法中，进而改变这些行为的时候不影响业务逻辑的代码

## 2. 一些名词

### 2.1. 目标对象（Target）

代理的目标对象。（被代理的对象）

### 2.2. 切面（Aspect）

抽象出的逻辑

### 2.3. 织入（Weaving）
  织入是把切面应用到目标对象并创建新的代理对象的过程。切面会在指定的连接点被织入到目标对象中。

## 3. 实现AOP的几种原理

2种：静态代理（编译时）和动态代理（运行时）

### 3.1. 静态代理

静态代理是指在编译时就已经确定了代理类和目标类的关系

流行类库：

- [PostSharp](https://www.postsharp.net/)（收费）
- Fody

### 3.2. 动态代理

动态代理是指在运行时动态生成代理类的字节码，并加载到内存中

流行类库：
- DynamicProxy



## 4. 实现

### 4.1. 静态代理-DIY

[说明](DiyStaticProxy/README.md)

### 4.2. 动态代理-DIY

[说明](DiyDynamicProxy/README.md)

### 4.3. 静态代理-Fody

todo

### 4.4. 动态代理-Castle.DynamicProxy

[说明](CastleDynamicProxySample/README.md)

### 4.5. 动态代理-Autofac + Castle.DynamicProxy

[说明](AutofacCastleDynamicProxySample/README.md)

## 5. Refers

https://www.cnblogs.com/wswind/p/aop_in_dotnet.html

根据网络搜索结果，.net实现AOP有以下几种方案：

- 使用Remoting Proxies，但是只能用于interface或MarshalByRefObjects¹
- 使用ContextBoundObject，但是性能非常低¹
- 使用编译时生成子类，如Rhino Proxy，但是只能用于Interfaces 或 virtual方法¹
- 使用运行时生成子类，如Castle Dynamic Proxy，但是实现复杂¹
- 使用挂钩分析用的API，如Type Mock，但是性能存在疑问¹
- 使用编译时IL修改，如Post Sharp / Cecil，但是非常难以实现¹²
- 使用运行时修改IL，如Post Sharp / Cecil，但是非常难以实现¹²

其中，Post Sharp是收费的商业项目²³，Castle Dynamic Proxy是开源免费的²。你可以根据你的需求和偏好选择合适的方案。

源: 与必应的对话， 4/12/2023
(1) 在.NET中实现AOP的7种姿势 - 腾讯云开发者社区-腾讯云. https://cloud.tencent.com/developer/article/1395543 访问时间 4/12/2023.
(2) AOP in .NET - wswind - 博客园. https://www.cnblogs.com/wswind/p/aop_in_dotnet.html 访问时间 4/12/2023.
(3) .NET中使用unity实现aop_DO_大龄码农的博客-CSDN博客. https://blog.csdn.net/xuwei_xuwei/article/details/33725059 访问时间 4/12/2023.
(4) .net5 AOP思想_小盆友你是否很有多问号的博客-CSDN博客. https://blog.csdn.net/qq_43369949/article/details/118353547 访问时间 4/12/2023.
(5) 利用C#实现AOP常见的几种方法详解 - PanPan003 - 博客园. https://www.cnblogs.com/panpanwelcome/p/8617552.html 访问时间 4/12/2023.