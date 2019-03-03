using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using NLog;

namespace Application.Dados
{
    public class EntityMonitorInterceptor : IDbCommandInterceptor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly int TempoLimiteConsultaMs = 1000;
        private readonly Stopwatch _cronometro = new Stopwatch();

        public void NonQueryExecuting(DbCommand comando, DbCommandInterceptionContext<int> contexto)
        {
            CommandExecuting(comando, contexto);
        }

        public void NonQueryExecuted(DbCommand comando, DbCommandInterceptionContext<int> contexto)
        {
            CommandExecuted(comando, contexto);
        }

        public void ReaderExecuting(DbCommand comando, DbCommandInterceptionContext<DbDataReader> contexto)
        {
            CommandExecuting(comando, contexto);
        }

        public void ReaderExecuted(DbCommand comando, DbCommandInterceptionContext<DbDataReader> contexto)
        {
            CommandExecuted(comando, contexto);
        }

        public void ScalarExecuting(DbCommand comando, DbCommandInterceptionContext<object> contexto)
        {
            CommandExecuting(comando, contexto);
        }

        public void ScalarExecuted(DbCommand comando, DbCommandInterceptionContext<object> contexto)
        {
            CommandExecuted(comando, contexto);
        }

        private void CommandExecuting<TResult>(DbCommand comando, DbCommandInterceptionContext<TResult> contexto)
        {
            _cronometro.Restart();
            LogTraceExecuting(comando);
            LogIfNonAsync(comando, contexto);
        }

        private void CommandExecuted<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> contexto)
        {
            _cronometro.Stop();
            LogTraceExecuted(_cronometro.Elapsed);
            LogIfError(command, contexto);
            LogIfTooSlow(command, _cronometro.Elapsed);
        }

        private void LogTraceExecuting(DbCommand comando)
        {
            var parametros = ObterParametrosComandoSql(comando);
            var texto = $"{comando.CommandText} - Parametros: {string.Join(", ", parametros)}";

            Logger.Trace(texto);
        }

        private void LogTraceExecuted(TimeSpan duracao)
        {
            var tempoExecucao = ArrendondarTempoExecucaoComandoSql(duracao.TotalMilliseconds / 1000d);
            Logger.Trace($"Tempo da consulta: {tempoExecucao}s");
        }

        private void LogIfNonAsync<TResult>(DbCommand comando, DbCommandInterceptionContext<TResult> contexto)
        {
            if (contexto.IsAsync) return;

            var parametros = ObterParametrosComandoSql(comando);
            var texto = $"Comando nao assincrono utilizado: {comando.CommandText} - Parametros: {string.Join(", ", parametros)}";

            Logger.Warn(texto);
        }

        private void LogIfError<TResult>(DbCommand comando, DbCommandInterceptionContext<TResult> contexto)
        {
            if (contexto.Exception == null) return;
            
            var parametros = ObterParametrosComandoSql(comando);
            var texto = $"Comando {comando.CommandText} falhou com a excecao {contexto.Exception} - Parametros: {string.Join(", ", parametros)}";

            Logger.Error(texto);
        }

        private void LogIfTooSlow(DbCommand command, TimeSpan completionTime)
        {
            if (completionTime.TotalMilliseconds <= TempoLimiteConsultaMs) return;

            var parametros = ObterParametrosComandoSql(command);
            var tempoExecucao = ArrendondarTempoExecucaoComandoSql(completionTime.TotalMilliseconds / 1000d);
            var tempoLimite = ArrendondarTempoExecucaoComandoSql(TempoLimiteConsultaMs / 1000d);

            var texto = $"Tempo da consulta ({tempoExecucao}s) excedeu o limite de {tempoLimite}s. Comando: \"{command.CommandText}\" - Parametros: {string.Join(", ", parametros)}";

            Logger.Warn(texto);
        }

        private IEnumerable<string> ObterParametrosComandoSql(DbCommand comando)
        {
            var parametros = new Dictionary<string, string>();
            foreach (DbParameter param in comando.Parameters)
            {
                parametros.Add(param.ParameterName, param.Value.ToString());
            }

            return parametros.Select(p => ":" + p.Key + " = " + p.Value);
        }

        private double ArrendondarTempoExecucaoComandoSql(double tempo)
        {
            return Math.Round(tempo, 3);
        }
    }
}