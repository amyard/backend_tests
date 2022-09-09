// See https://aka.ms/new-console-template for more information

using System.Xml;

string zeroDecimalValue = "0.00";
string zeroDecimalValuePrecission = "0.0000";
List<ProductAttributeParcedValue> result = new List<ProductAttributeParcedValue>();

var attributeXml = @"<Attributes><ProductAttribute ID=""38315""><ProductAttributeValue><Value>211005</Value><PriceAdjustment>0.0000</PriceAdjustment><Cost>0.0000</Cost><FlatFeePrice>0.00</FlatFeePrice><FlatFeeCost>0.00</FlatFeeCost></ProductAttributeValue></ProductAttribute><ProductAttribute ID=""38314""><ProductAttributeValue><Value>210969</Value><PriceAdjustment>0.0000</PriceAdjustment><Cost>0.0000</Cost><FlatFeePrice>0.00</FlatFeePrice><FlatFeeCost>0.00</FlatFeeCost></ProductAttributeValue></ProductAttribute><ProductAttribute ID=""38317""><ProductAttributeValue><Value>some line 1</Value><PriceAdjustment>0.00</PriceAdjustment><Cost>0.00</Cost><FlatFeePrice>0.00</FlatFeePrice><FlatFeeCost>0.00</FlatFeeCost></ProductAttributeValue><ProductAttributeValue><Value>line 2 of 2</Value><PriceAdjustment>0.00</PriceAdjustment><Cost>0.00</Cost><FlatFeePrice>0.00</FlatFeePrice><FlatFeeCost>0.00</FlatFeeCost></ProductAttributeValue></ProductAttribute><ProductAttribute ID=""38319""><ProductAttributeValue><Value>211009</Value><PriceAdjustment>0.4000</PriceAdjustment><Cost>0.2500</Cost><FlatFeePrice>15.00</FlatFeePrice><FlatFeeCost>10.00</FlatFeeCost></ProductAttributeValue><ProductAttributeValue><Value>668004</Value><PriceAdjustment>0.0000</PriceAdjustment><Cost>0.0000</Cost><FlatFeePrice>12.00</FlatFeePrice><FlatFeeCost>23.00</FlatFeeCost></ProductAttributeValue><ProductAttributeValue><Value>667953</Value><PriceAdjustment>0.2000</PriceAdjustment><Cost>0.1500</Cost><FlatFeePrice>12.00</FlatFeePrice><FlatFeeCost>8.00</FlatFeeCost></ProductAttributeValue></ProductAttribute><ProductAttribute ID=""38316""><ProductAttributeValue><Value>Comments &amp; Instruction (Date Need Order, Imprint Details - Additional Imprint Colors, Layout, Fonts)</Value><PriceAdjustment>0.00</PriceAdjustment><Cost>0.00</Cost><FlatFeePrice>0.00</FlatFeePrice><FlatFeeCost>0.00</FlatFeeCost></ProductAttributeValue></ProductAttribute></Attributes>";



var xmlDoc = new XmlDocument();
xmlDoc.LoadXml(attributeXml);

var nodeList1 = xmlDoc.SelectNodes(@"//Attributes/ProductAttribute");
foreach (XmlNode attributeNode in nodeList1)
{
    if (attributeNode.Attributes == null || attributeNode.Attributes["ID"] == null) continue;
    string str1 = attributeNode.Attributes["ID"].InnerText.Trim();
    int.TryParse(str1, out int id);
    
    foreach (XmlNode attributeValue in attributeNode.SelectNodes("ProductAttributeValue"))
    {
        var value = attributeValue.SelectSingleNode("Value").InnerText.Trim();
        var quantityNode = attributeValue.SelectSingleNode("Quantity");
        var cost = attributeValue.SelectSingleNode("Cost")?.InnerText?.Trim();
        var priceAdjustment = attributeValue.SelectSingleNode("PriceAdjustment")?.InnerText?.Trim();
        var flatFeePrice = attributeValue.SelectSingleNode("FlatFeePrice")?.InnerText?.Trim();
        var flatFeeCost = attributeValue.SelectSingleNode("FlatFeeCost")?.InnerText?.Trim();
                                
        result.Add(new ProductAttributeParcedValue()
        {
            AttrId = value,
            Quantity = quantityNode != null ? quantityNode.InnerText.Trim() : string.Empty,
            Cost = cost,
            PriceAdjustment = priceAdjustment,
            FlatFeeCost = flatFeeCost,
            FlatFeePrice = flatFeePrice
        });
    }
}

var asd = "aaa";

public class ProductAttributeParcedValue 
{
    // can be int for Id or string as TextField
    public string AttrId { get; set; }
    public string Quantity { get; set; }
    public string Cost { get; set; }
    public string PriceAdjustment { get; set; }
    public string FlatFeePrice { get; set; }
    public string FlatFeeCost { get; set; }
    public int ProductAttributeMappingId { get; set; }
}

    

