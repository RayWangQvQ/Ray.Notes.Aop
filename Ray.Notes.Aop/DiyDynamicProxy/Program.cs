using Shares;
using System.Reflection.Emit;
using System.Reflection;

namespace DiyDynamicProxy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var type = CreateDynamicProxyType();
            var dynamicProxy = (ISpeakService)Activator.CreateInstance(type, new object[] { new SpeakService() });
            dynamicProxy.Say();
        }

        static Type CreateDynamicProxyType()
        {
            var assemblyName = new AssemblyName("SomeAssemblyName");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var modBuilder = assemblyBuilder.DefineDynamicModule("SomeAssemblyName");

            var typeBuilder = modBuilder.DefineType(
                "MyDynamicProxy",
                TypeAttributes.Public | TypeAttributes.Class,
                typeof(object),
                new[] { typeof(ISpeakService) });

            //私有字段
            var fieldBuilder = typeBuilder.DefineField(
                "_targetObject",
                typeof(SpeakService),
                FieldAttributes.Private);

            //构造函数
            var constructorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.HasThis,
                new[] { typeof(SpeakService) });
            var contructorIl = constructorBuilder.GetILGenerator();
            contructorIl.Emit(OpCodes.Ldarg_0);
            contructorIl.Emit(OpCodes.Ldarg_1);
            contructorIl.Emit(OpCodes.Stfld, fieldBuilder);
            contructorIl.Emit(OpCodes.Ret);

            //Say方法
            var methodBuilder = typeBuilder.DefineMethod("Say",
                                MethodAttributes.Public | MethodAttributes.Virtual,
                                typeof(void),
                                null);
            typeBuilder.DefineMethodOverride(methodBuilder, typeof(ISpeakService).GetMethod("Say"));
            var method1 = methodBuilder.GetILGenerator();

            //pre
            method1.Emit(OpCodes.Ldstr, "咳咳咳");
            method1.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));

            //say
            method1.Emit(OpCodes.Ldarg_0);//load arg0 (this)
            method1.Emit(OpCodes.Ldfld, fieldBuilder);//load _realObject
            //method1.Emit(OpCodes.Ldarg_1);//load argument1
            method1.Emit(OpCodes.Call, fieldBuilder.FieldType.GetMethod("Say"));//call Method1

            //after
            method1.Emit(OpCodes.Ldstr, "感谢大家");
            method1.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
            method1.Emit(OpCodes.Ret);
            return typeBuilder.CreateType();
        }
    }
}