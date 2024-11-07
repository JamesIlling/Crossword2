﻿using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;


namespace Crossword.Test
{
    public class ProgramTests
    {
        [Theory]
        [InlineData(typeof(IConsole), typeof(ConsoleWrapper))]
        [InlineData(typeof(ICrossword), typeof(GuardianCrossword))]
        [InlineData(typeof(IPdfMerger), typeof(PdfMerger))]
        public void EnsureServiceAreRegistered(Type service, Type implementation)
        {
            var services = new ServiceCollection();
            Program.ConfigureServices(services);
            var serviceDescriptors = services.ToList();
            serviceDescriptors.Where(x => x.ServiceType == service && x.ImplementationType == implementation).Should()
                .NotBeEmpty();

        }

        [Theory]
        [InlineData(typeof(HttpClient))]
        [InlineData(typeof(Crosswords))]
        public void EnsureServiceInstancesAreRegistered(Type implementation)
        {
            var services = new ServiceCollection();
            Program.ConfigureServices(services);
            var serviceDescriptors = services.ToList();
            serviceDescriptors.Where(x => x.ImplementationType == implementation).Should()
                .NotBeEmpty();

        }
    }
}