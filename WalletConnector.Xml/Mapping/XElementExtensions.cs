using System;
using System.Xml.Linq;

namespace WalletConnectior.Xml.Mapping
{
    public static class XElementExtensions
    {
        public static string AsString(this XElement e) => 
            e == null || e.IsEmpty ? string.Empty : e.Value;
        public static decimal AsDecimal(this XElement e) =>
            e == null || e.IsEmpty ? 0m : decimal.Parse(e.Value);
        public static long AsLong(this XElement e) =>
            e == null || e.IsEmpty ? 0L : long.Parse(e.Value);
        public static Guid AsGuid(this XElement e) =>
            e == null || e.IsEmpty ? Guid.Empty : Guid.Parse(e.Value);
        
        public static string AsString(this XAttribute e) => 
            e == null ? string.Empty : e.Value;
        public static decimal AsDecimal(this XAttribute e) =>
            e == null ? 0m : decimal.Parse(e.Value);
        public static long AsLong(this XAttribute e) =>
            e == null ? 0L : long.Parse(e.Value);
        public static Guid AsGuid(this XAttribute e) =>
            e == null ? Guid.Empty : Guid.Parse(e.Value);
    }
}