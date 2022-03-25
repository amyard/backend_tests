// See https://aka.ms/new-console-template for more information

using System;
using HashidsNet;

Console.WriteLine("Hello, World!");

// for web api config
// builder.Services.AddSingleton<IHashids>(_ => new Hashids("maksim", 11));

// 1
var hashIds = new Hashids("maksim", 11);

var firstValue = hashIds.Encode(1);
var secondValue = hashIds.Encode(2,3,4);

Console.WriteLine(firstValue);
Console.WriteLine(secondValue);

var firstDecodeValue = hashIds.Decode("j5g0bwBry2P");
var secondDecodeValue = hashIds.Decode("1ZrBKHxt3b7");


Console.WriteLine(firstDecodeValue[0]);
Console.WriteLine(secondDecodeValue[0]);
Console.WriteLine(secondDecodeValue[1]);
Console.WriteLine(secondDecodeValue[2]);
// Console.WriteLine(secondDecodeValue[3]); - EXCEPTION HERE!!!



// 2
// alphabet: "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890", 
// seps: "cfhistuCFHISTU"

var hashIds2 = new Hashids("maksim", 11,
    alphabet: "abcdefghijklmnopqrstuvwxyz", 
    seps: "cfhistuCFHISTU");

var firstValue2 = hashIds2.Encode(1);
var secondValue2 = hashIds2.Encode(2,3,4);

Console.WriteLine(firstValue2);  // xdgyvnmbolj
Console.WriteLine(secondValue2); // dpbaoigcpbr


var hashIds3 = new Hashids("maksim", 11,
    alphabet: "abcdefghijklmnopqrstuvwxyz", 
    seps: "dcfhistuDCFHISTU");

var firstValue3 = hashIds3.Encode(1);
var secondValue3 = hashIds3.Encode(2,3,4);

Console.WriteLine(firstValue3);  // zpqemvnmaow
Console.WriteLine(secondValue3); // pgmxvhqdnmq


// ERROR here  System.DivideByZeroException:
var hashIds4 = new Hashids("maksim", 11,
    alphabet: "abcdefghijklmnopqrstuvwxyz", 
    seps: "abcdefghijklmnopqrstuvwxyz");

var firstValue4 = hashIds4.Encode(1);
var secondValue4 = hashIds4.Encode(2,3,4);

Console.WriteLine(firstValue4);  // zpqemvnmaow
Console.WriteLine(secondValue4); // pgmxvhqdnmq