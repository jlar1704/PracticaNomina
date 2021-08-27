using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Practica2_Nomina.Pages
{
    public class empleado
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cargo { get; set; }
        public double Sueldo { get; set; }

        public double AFP
        {
            get { return CalcularAFP(Sueldo); }
        }

        public double ARS
        {
            get { return CalcularARS(Sueldo); }
        }

        public double ISR
        {
            get { return CalcularISR(Sueldo - AFP - ARS); }
        }

        private double CalcularARS(double sueldo)
        {
            double TopeAFP = 13482.00 * 10;

            if (sueldo > TopeAFP)
            {
                sueldo = TopeAFP;
            }

            return (sueldo * 3.04) / 100;
        }

        private double CalcularAFP(double sueldo)
        {
            double TopeARS = 13482.00 * 20;

            if (sueldo > TopeARS)
            {
                sueldo = TopeARS;
            }

            return (sueldo * 2.87) / 100;
        }

        private double CalcularISR(double sueldo)
        {
            double ISR = 0;

            if ((sueldo >= 34685) && (sueldo <= 52027.42))
            {
                ISR = ((sueldo - 34685) * 15 / 100);
            }
            else if ((sueldo >= 52027.43) && (sueldo <= 72260.25))
            {
                ISR = 2601.33 + ((sueldo - 52027.43) * 20 / 100);
            }
            else if (sueldo >= 72260.25)
            {
                ISR = 6648.00 + ((sueldo - 72260.25) * 25 / 100);
            }

            return ISR;
        }
    }


    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        public List<empleado> empleados { get; set; }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            this.empleados = new List<empleado>();

            for (int i = 4; i <= 15; i++)
            {
                this.empleados.Add(
                    new empleado()
                    {

                        Nombre = "Empleado " + (i - 3).ToString(),
                        Apellido = "Sueldo " + string.Format("{0:#,0.00}", i * 10000),
                        Cargo = "Programador Web",
                        Sueldo = (i * 10000)
                    }
                    );

            }
        }

        public void OnGet()
        {

        }
    }
}
