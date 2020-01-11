//public class HintCommandInterceptor : DbCommandInterceptor
//{
//    public override InterceptionResult ReaderExecuting(
//        DbCommand command,
//        CommandEventData eventData,
//        InterceptionResult result)
//    {
//        // Manipulate the command text, etc. here...
//        command.CommandText += " OPTION (OPTIMIZE FOR UNKNOWN)";
//        return result;
//    }
//}

//services.AddDbContext(b => b
//    .UseSqlServer(connectionString)
//    .AddInterceptors(new HintCommandInterceptor()));