using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Eppendorf_FSC.Mapping
{
    internal class FilePriceConverter : IValueConverter<string, double>
    {
        public double Convert(string sourceMember, ResolutionContext context)
        {
            return double.Parse(sourceMember,CultureInfo.InvariantCulture);
        }
    }
}
