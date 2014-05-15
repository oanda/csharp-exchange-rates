using System;
using Oanda;
using System.Collections.Generic;

public class OANDAExchangeRates_Main
{
    public static void Main(string[] args)
    {
        // Initializing the API
        var api = new ExchangeRates("<API KEY>");

        // Example of listing currencies

        Console.WriteLine("\n********** Listing Currencies:");
        var responseCurrencies = api.GetCurrencies();

        if (responseCurrencies.IsSuccessful)
        {
            foreach (var currency in responseCurrencies.Currencies)
            {
                Console.WriteLine("{0}:{1}", currency.Code, currency.Description);
            }
        }
        else
        {
            Console.WriteLine("There was an error with the request: {0}", responseCurrencies.ErrorMessage);
        }


        // Example of checking RemainingQuotes

        var responseRemaingQuotes = api.GetRemainingQuotes();

        Console.WriteLine("\n********** Remaining Quotes:");
        if (responseRemaingQuotes.IsSuccessful)
        {
            Console.WriteLine("Remaining quotes: {0}", responseRemaingQuotes.RemainingQuotes);
        }
        else
        {
            Console.WriteLine("There was an error with the request: {0}", responseRemaingQuotes.ErrorMessage);
        }


        // Example of retrieving rates by specifying multiple fields.

        var responseGetRatesWithFields = api.GetRates("USD",
                quote: "EUR",
                start: "2014-01-01",
                end: "2014-01-05",
                fields: new List<ExchangeRates.RatesFields> { ExchangeRates.RatesFields.Averages, ExchangeRates.RatesFields.Midpoint });

        if (responseGetRatesWithFields.IsSuccessful)
        {
            Console.WriteLine("\n********** Rates - Averages and Midpoints:");
            foreach (var quote in responseGetRatesWithFields.Quotes)
            {
                Console.WriteLine("From {0} to {1}\nQuote:{2} Ask:{3,10} MidPoint:{4,10} Bid:{5,10}",
                        responseGetRatesWithFields.Meta.EffectiveParams.StartDate.Value.ToShortDateString(),
                        responseGetRatesWithFields.Meta.EffectiveParams.EndDate.Value.ToShortDateString(),
                        quote.Key,
                        quote.Value.Ask,
                        quote.Value.Midpoint,
                        quote.Value.Bid);

            }
        }
        else
        {
            Console.WriteLine("There was an error with the request: {0}", responseGetRatesWithFields.ErrorMessage);
        }

        // Example of retrieving rates by specifying multiple quotes.

        var responseGetRatesForSeveralQuotes = api.GetRates("USD",
                quote: new List<string> {"EUR", "CHF"} ,
                date: "2014-01-01",
                fields: ExchangeRates.RatesFields.Averages);

        if (responseGetRatesForSeveralQuotes.IsSuccessful)
        {
            Console.WriteLine("\n********** Rates for USD/EUR and USD/CHF:");
            foreach (var quote in responseGetRatesForSeveralQuotes.Quotes)
            {
                Console.WriteLine("{0} Quote:{1} Ask:{2,10} Bid:{3,10}",
                        quote.Value.Date,
                        quote.Key,
                        quote.Value.Ask,
                        quote.Value.Bid);

            }
        }
        else
        {
            Console.WriteLine("There was an error with the request: {0}", responseGetRatesWithFields.ErrorMessage);
        }

        Console.ReadKey();

    }
}

