﻿namespace BestWeatherForecast.Domain;

using System.Collections.Generic;
using FunctionalDDD.DomainDrivenDesign;

public class WeatherForecast : AggregateRoot<ZipCode>
{
    private readonly List<DailyTemperature> dailyTemperatures = new();

    public IReadOnlyList<DailyTemperature> DailyTemperatures { get => dailyTemperatures; }

    public WeatherForecast(ZipCode zipCode, IEnumerable<DailyTemperature> dailyTemperatures) : base(zipCode)
        => this.dailyTemperatures.AddRange(dailyTemperatures);
}
