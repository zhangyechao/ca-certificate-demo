var list = new List<string>
{ 
    "阿莫西林", "阿司匹林", "阿卡波糖"
};

Console.WriteLine("Sort by default culture , {0}", Thread.CurrentThread.CurrentCulture.Name);
list.Sort();

foreach (var item in list)
{
    Console.WriteLine(item);
}

Console.WriteLine("");

Console.WriteLine("Sort by culture , en-US");
list.Sort(StringComparer.Create(new System.Globalization.CultureInfo("en-US"), true));
foreach (var item in list)
{
    Console.WriteLine(item);
}
Console.WriteLine("");


Console.WriteLine("Sort by culture , zh-Hans-CN");
list.Sort(StringComparer.Create(new System.Globalization.CultureInfo("zh-Hans-CN"), true));
foreach (var item in list)
{
    Console.WriteLine(item);
}
Console.WriteLine("");

Console.WriteLine($"CurrentCulture Name = {System.Globalization.CultureInfo.CurrentCulture.Name}"); 

var all = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures);
foreach (var item in all)
{
    Console.WriteLine(item.Name);
}
