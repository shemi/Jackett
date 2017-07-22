﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Jackett.Utils
{
    public class NonNullException : Exception
    {
        public NonNullException() : base("Parameter cannot be null")
        {
        }
    }

    public class NonNull<T> where T : class
    {
        public NonNull(T val)
        {
            if (val == null)
                new NonNullException();

            Value = val;
        }

        public static implicit operator T(NonNull<T> n)
        {
            return n.Value;
        }

        private T Value;
    }

    public static class GenericConversionExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this T obj)
        {
            return new T[] { obj };
        }

        public static NonNull<T> ToNonNull<T>(this T obj) where T : class
        {
            return new NonNull<T>(obj);
        }
    }

    public static class EnumerableExtension
    {
        public static string AsString(this IEnumerable<char> chars)
        {
            return String.Concat(chars);
        }
    }

    public static class StringExtension
    {
        public static bool IsNullOrEmptyOrWhitespace(this string str)
        {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }

        public static DateTime ToDateTime(this string str)
        {
            return DateTime.Parse(str);
        }

        public static Uri ToUri(this string str)
        {
            return new Uri(str);
        }
    }

    public static class CollectionExtension
    {
        public static bool IsEmpty<T>(this ICollection<T> obj)
        {
            return obj.Count == 0;
        }

        public static bool IsEmptyOrNull<T>(this ICollection<T> obj)
        {
            return obj == null || obj.IsEmpty();
        }
    }

    public static class XElementExtension
    {
        public static XElement First(this XElement element, string name)
        {
            return element.Descendants(name).First();
        }

        public static string FirstValue(this XElement element, string name)
        {
            return element.First(name).Value;
        }
    }
}