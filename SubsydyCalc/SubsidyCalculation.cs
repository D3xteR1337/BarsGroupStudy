using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubsydyCalc
{
    class SubsidyCalculation : ISubsidyCalculation
    {
        public event EventHandler<string> OnNotify;
        public event EventHandler<Tuple<string, Exception>> OnException;

        public Charge CalculateSubsidy(Volume volumes, Tariff tariff)
        {
            OnNotify("count", "start");
            Charge resCharge = new Charge();

            if (volumes.HouseId != tariff.HouseId)
            {
                Exception e = new Exception("Разные дома");
                Tuple<string, Exception> out_t = new Tuple<string, Exception>("difhouse", e);
                OnException("difException", out_t);
                throw e;
            }

            if (volumes.ServiceId != tariff.ServiceId)
            {
                Exception e = new Exception("Разные услуги");
                Tuple<string, Exception> out_t = new Tuple<string, Exception>("difservice", e);
                OnException("difException", out_t);
                throw e;
            }
            try
            {
                resCharge.Month = volumes.Month;
                resCharge.HouseId = volumes.HouseId;
                resCharge.ServiceId = volumes.ServiceId;
                int MonthDif = tariff.PeriodEnd.Month - tariff.PeriodBegin.Month;
                resCharge.Value = (MonthDif * tariff.Value * volumes.Value);
            }
            catch (Exception e)
            {
                Tuple<string, Exception> out_t = new Tuple<string, Exception>("someException", e);
                OnException("someException", out_t);
                throw;
            }

            OnNotify("count", "end");
            return resCharge;
        }

        
    }
}
