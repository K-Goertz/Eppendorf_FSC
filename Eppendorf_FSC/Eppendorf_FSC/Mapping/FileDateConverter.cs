using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Eppendorf_FSC.Mapping
{
    internal class FileDateConverter : IValueConverter<string, DateTime>
    {
        public DateTime Convert(string sourceMember, ResolutionContext context)
        {
            return DateTime.ParseExact(sourceMember, "d", CultureInfo.InvariantCulture);
        }
    }
}
