// TODO: could allow the user to dynamically specify an input value, consider negatives though
ulong number = 255;

Console.WriteLine($"   Binary: {ToBinary(number)}");
Console.WriteLine($"      Hex: {ToHex(number)}");
Console.WriteLine($"    Octal: {ToBase(number, 8, "o", division:3)}");
Console.WriteLine($"  Decimal: {ToBase(number, 10, division: 3, separator: ",")}");
Console.WriteLine($"   Binary: {ToBase(number, 2, prefix:"0b")}");
Console.WriteLine($"      Hex: {ToBase(number, 16, postfix:"_16")}");
Console.WriteLine($"  Base 36: {ToBase(number, 36, digits:"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ")}");
Console.WriteLine(
    $"Arbitrary: {ToBase(number, 42, prefix: ":)", digits: "01234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefg")}");
Console.WriteLine($"   Base 5: {ToBase(number, 5, prefix: "5x")}");
return;

static string ToHex(ulong number)
{
    if (number == 0)
    {
        return "0x0";
    }

    const string hexDigits = "0123456789ABCDEF";
    string hex = "";

    while (number > 0)
    {
        int remainder = (int)number % 16;
        hex = hexDigits[remainder] + hex;
        number /= 16;
    }

    for (int i = hex.Length - 4; i > 0; i -= 4)
    {
        hex = hex.Insert(i, " ");
    }


    hex = "0x" + hex;

    return hex;
}

static string ToBinary(ulong number)
{
    if (number == 0)
    {
        return "0x0";
    }

    string binary = "";

    while (number > 0)
    {
        int remainder = (int)number % 2;
        binary = remainder + binary;
        number /= 2;
    }

    for (int i = binary.Length - 4; i > 0; i -= 4)
    {
        binary = binary.Insert(i, " ");
    }

    binary += 'b';

    return binary;
}

static string ToBase(ulong number,
    uint @base,
    string prefix = "",
    string postfix = "",
    string digits = "0123456789ABCDEF",
    int division = 4,
    string separator = " ")
{
    if (@base > digits.Length)
    {
        throw new ArgumentException("There must be enough digits to cover the base");
    }

    if (@base < 1)
    {
        throw new ArgumentException("Base must be greater than 0", nameof(@base));
    }

    if (number == 0)
    {
        return $"{prefix}0{postfix}";
    }

    // TODO: should throw an exception if base is too large, say 1000?

    // TODO: Should use string builder but that is left for another time
    string result = "";

    // Algorithm
    // While number is greater than 0, get the remainder of that number divided by the base.
    //  Then get the base representation of that number, insert it at the front of the result.
    //  Then actually perform the division by base and use that as the new value of number.
    //  This makes use of the parameter number rather than creating a new variable for it.

    while (number > 0)
    {
        // This case is okay because despite number being larger than an int, the modulo by the base results
        //   in a value within in the range of the base, which can be too long but I would argue not in scope
        //   during this example.
        int remainder = (int)(number % @base);
        result = digits[remainder] + result;
        number /= @base;
    }

    for (int i = result.Length - division; i > 0; i -= division)
    {
        // Uses string method Insert to insert "separator" into the string at position "i"
        result = result.Insert(i, separator);
    }

    return $"{prefix}{result}{postfix}";
}
