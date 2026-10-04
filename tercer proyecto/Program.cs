using Spectre.Console;

        AnsiConsole.MarkupLine("[bold green]TABLA DE AMORTIZACIÓN DE PRÉSTAMO[/]");
        AnsiConsole.WriteLine();

        // Solicitar datos
        decimal montoPrestamo = AnsiConsole.Ask<decimal>(
            "[yellow]Ingrese el monto del préstamo:[/]");

        decimal tasaAnual = AnsiConsole.Ask<decimal>(
            "[red]Ingrese la tasa de interés anual (%):[/]");

        int plazoMeses = AnsiConsole.Ask<int>(
            "[yellow]Ingrese el plazo del préstamo (meses):[/]");

        // Convertir tasa anual a mensual
        decimal tasaMensual = (tasaAnual / 12m) / 100m;

        // Fórmula de cuota fija
        double M = (double)montoPrestamo;
        double i = (double)tasaMensual;
        int n = plazoMeses;

        double cuotaDouble =
            M * i * Math.Pow(1 + i, n)
            / (Math.Pow(1 + i, n) - 1);

        decimal cuota = (decimal)cuotaDouble;

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine(
            $"[bold blue]Cuota fija mensual: RD${cuota:N2}[/]");
        AnsiConsole.WriteLine();

        // Crear tabla
        var tabla = new Table();

        tabla.Border(TableBorder.Rounded);

        tabla.AddColumn("No. Cuota");
        tabla.AddColumn("Pago de Cuota");
        tabla.AddColumn("Interés");
        tabla.AddColumn("Abono Capital");
        tabla.AddColumn("Saldo");

        decimal saldo = montoPrestamo;

        for (int mes = 1; mes <= plazoMeses; mes++)
        {
            decimal interes = saldo * tasaMensual;
            decimal abonoCapital = cuota - interes;

            saldo -= abonoCapital;

            if (saldo < 0)
                saldo = 0;

            tabla.AddRow(
                mes.ToString(),
                $"RD${cuota:N2}",
                $"RD${interes:N2}",
                $"RD${abonoCapital:N2}",
                $"RD${saldo:N2}"
            );
        }

        AnsiConsole.Write(tabla);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine(
            "[green]Proceso finalizado correctamente.[/]");

        Console.ReadKey();