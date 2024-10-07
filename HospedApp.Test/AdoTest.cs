using HospedApp.Core;
using HospedApp.Dapper;

namespace HospedApp.Test;

public class AdoTest
{
    protected readonly IAdo Ado;
    private const string _cadena = "Server=localhost;Database=HospedApp;Uid=5to_agbd;pwd=Trigg3rs!;Allow User Variables=True";
    public AdoTest() => Ado = new AdoDapper(_cadena);
    public AdoTest(string cadena) => Ado = new AdoDapper(cadena);
}