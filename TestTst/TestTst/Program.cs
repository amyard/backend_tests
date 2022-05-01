
using System.Globalization;


// Currency value

string a1 = "$35.00";
string a2 = "$35,00";
string a3 = "$0.00";


decimal q1 = decimal.Parse(a1, NumberStyles.AllowCurrencySymbol | NumberStyles.Number, CultureInfo.CreateSpecificCulture("us-US").NumberFormat);
Console.WriteLine(q1);

decimal q2 = decimal.Parse(a2, NumberStyles.AllowCurrencySymbol | NumberStyles.Number, CultureInfo.CreateSpecificCulture("us-US").NumberFormat);
Console.WriteLine(q2);

decimal q3 = decimal.Parse(a3, NumberStyles.AllowCurrencySymbol | NumberStyles.Number, CultureInfo.CreateSpecificCulture("us-US").NumberFormat);
Console.WriteLine(q3);

Console.WriteLine(default(decimal));
Console.WriteLine("------------------------");

Console.WriteLine(q1 == default(decimal));
Console.WriteLine(q2 == default(decimal));
Console.WriteLine(q3 == default(decimal));


// Percentage value

var w1 = "20.00%";
var w2 = "20,00%";
var w3 = "0.00%";

var e1 = decimal.Parse(w1.Replace("%",""), CultureInfo.CreateSpecificCulture("us-US").NumberFormat);
var e2 = decimal.Parse(w2.Replace("%",""), CultureInfo.CreateSpecificCulture("us-US").NumberFormat);
var e3 = decimal.Parse(w3.Replace("%",""), CultureInfo.CreateSpecificCulture("us-US").NumberFormat);


Console.WriteLine("------------------------");
Console.WriteLine(e1);
Console.WriteLine(e2);
Console.WriteLine(e3);


Console.WriteLine("------------------------");
Console.WriteLine(e1 == default(decimal));
Console.WriteLine(e2 == default(decimal));
Console.WriteLine(e3 == default(decimal));


Console.WriteLine("Hello, World!");
