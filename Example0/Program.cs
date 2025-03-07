using ASiNet.Extensions.DevTools;
using ASiNet.Extensions.DevTools.DebugTools;
using ASiNet.Extensions.DevTools.Enums;
using ASiNet.Extensions.DevTools.Interfaces;

IAssociation? association = null;


using (_ = SpeedTest.New(x => Console.WriteLine($"Init time: {x.Milliseconds}ms\n{x.Ticks}t\n"), true))
{
    association = Associations.CreateNew("Test")
        .Mode(AssociationMode.TwoWay)
        .AddTransient<A, B>()
        .AddTransient<A1, B1>()
        .AddTransient<A2, B2>()
        .AddTransient<A3, B3>()
        .AddTransient<A4, B4>()
        .AddTransient(typeof(A5), typeof(B5))
        .Build();
}



var a = association.GetAssociation<A, B>();

var a2 = association.GetAssociation<B2>();

var b3 = association.GetAssociation<B3>(typeof(A3));

var a5 = association.GetAssociation(typeof(B5));

Console.ReadKey();







public class A { } 
public class B { }
public class A1 { }
public class B1 { }
public class A2 { }
public class B2 { }
public class A3 { }
public class B3 { }
public class A4 { }
public class B4 { }
public class A5 { }
public class B5 { }