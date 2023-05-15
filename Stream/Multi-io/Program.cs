﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiIo;
using Steeltoe.Stream.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Host.AddStreamServices(typeof(SampleSource), typeof(SampleSink));
builder.Services.AddHostedService<PollerService>();
builder.Build().Run();
