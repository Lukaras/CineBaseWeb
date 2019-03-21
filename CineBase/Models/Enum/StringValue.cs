using System;

public sealed class StringValueAttribute : Attribute
{
    /// <summary>
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value"></param>
    public StringValueAttribute(string value)
    {
        StringValue = value;
    }

    /// <summary>
    /// Holds the stringvalue for a value in an enum.
    /// </summary>
    public string StringValue { get; protected set; }
}
