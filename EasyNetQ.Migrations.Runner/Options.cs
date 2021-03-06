﻿namespace EasyNetQ.Migrations.Runner {
    using System;
    using CommandLine;

    class Options {
        [Option('h', "hostUrl", Required = true, HelpText = "RabbitMQ broker host url.")]
        public String HostUrl { get; set; }

        [Option('P', "port", Default = 15672, HelpText = "RabbitMQ broker port number.")]
        public Int32 Port { get; set; }

        [Option('u', "username", Default = "guest", HelpText = "RabbitMQ broker login username.")]
        public String Username { get; set; }

        [Option('p', "password", Default = "guest", HelpText = "RabbitMQ broker login password.")]
        public String Password { get; set; }

        [Value(0, Required = true, HelpText = "Path to the assembly containing the migrations.")]
        public String MigrationsAssemblyPath { get; set; }

        [Option('v', "version", Default = 0, HelpText = "Migrations version number. Any migrations in the assembly with a version number greater than this parameter will be applied.")]
        public Int64 Version { get; set; }

        [Option('l', "logfile", Required = false, HelpText = "Log output to file.")]
        public String LogFilePath { get; set; }

        [Option('d', "dryrun", Default = false, HelpText = "Execute migration as a dry run. No changes will be made to your broker.")]
        public Boolean DryRun { get; set; }
    }
}