using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApplication3.Dtos;
using WebApplication3.Entities;

namespace WebApplication3.Formatters
{
    public class CarVCardOutputFormatter : TextOutputFormatter
    {
        public CarVCardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if(typeof(CarDto).IsAssignableFrom(type))
                return true;

            if (typeof(IEnumerable<CarDto>).IsAssignableFrom(type))
                return true;


            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            if(context.Object is IEnumerable<CarDto> cars)
            {
                foreach (var car in cars)
                {
                    await WriteVCard(car, response);
                }
            }
            else
            {
                await WriteVCard((CarDto)context.Object!, response);
            }
        }

        private static Task WriteVCard(CarDto car,HttpResponse response)
        {
            return response.WriteAsync($@"
                BEGIN:VCARD
                VERSION:3.0
                FN:{car.Model} {car.Vendor}
                NOTE:Year={car.Year}
                NOTE:Engine={car.Engine}
                END:VCARD
");
        }
    }
}
