﻿using Microsoft.Extensions.Logging;
using System.Text;

namespace Furion.Logging;

/// <summary>
/// 文件日志记录器
/// </summary>
/// <remarks>https://docs.microsoft.com/zh-cn/dotnet/core/extensions/custom-logging-provider</remarks>
[SuppressSniffer]
public sealed class FileLogger : ILogger
{
    /// <summary>
    /// 记录器类别名称
    /// </summary>
    private readonly string _logName;

    /// <summary>
    /// 文件记录器提供器
    /// </summary>
    private readonly FileLoggerProvider _fileLoggerProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logName">记录器类别名称</param>
    /// <param name="fileLoggerProvider">文件记录器提供器</param>
    public FileLogger(string logName, FileLoggerProvider fileLoggerProvider)
    {
        _logName = logName;
        _fileLoggerProvider = fileLoggerProvider;
    }

    /// <summary>
    /// 日志上下文
    /// </summary>
    public LogContext Context { get; private set; }

    /// <summary>
    /// 开始逻辑操作范围
    /// </summary>
    /// <typeparam name="TState">标识符类型参数</typeparam>
    /// <param name="state">要写入的项/对象</param>
    /// <returns><see cref="IDisposable"/></returns>
    public IDisposable BeginScope<TState>(TState state)
    {
        // 设置日志上下文
        if (state is LogContext context)
        {
            Context = context;
        }

        return default;
    }

    /// <summary>
    /// 检查是否已启用给定日志级别
    /// </summary>
    /// <param name="logLevel">日志级别</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= _fileLoggerProvider.MinimumLevel;
    }

    /// <summary>
    /// 写入日志项
    /// </summary>
    /// <typeparam name="TState">标识符类型参数</typeparam>
    /// <param name="logLevel">日志级别</param>
    /// <param name="eventId">事件 Id</param>
    /// <param name="state">要写入的项/对象</param>
    /// <param name="exception">异常对象</param>
    /// <param name="formatter">日志格式化器</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Log<TState>(LogLevel logLevel
        , EventId eventId
        , TState state
        , Exception exception
        , Func<TState, Exception, string> formatter)
    {
        // 判断日志级别是否有效
        if (!IsEnabled(logLevel)) return;

        // 检查日志格式化器
        if (formatter == null) throw new ArgumentNullException(nameof(formatter));

        // 获取格式化后的消息
        var message = formatter(state, exception);
        var logMsg = new LogMessage(_logName, logLevel, eventId, message, exception, Context);

        // 是否自定义了日志筛选器，如果是则检查是否条件
        if (_fileLoggerProvider.LoggerOptions.WriteFilter?.Invoke(logMsg) == false) return;

        // 是否自定义了自定义日志格式化程序，如果是则使用
        if (_fileLoggerProvider.MessageFormat != null)
        {
            // 写入日志队列
            _fileLoggerProvider.WriteToQueue(_fileLoggerProvider.MessageFormat(logMsg));

            return;
        }

        // 创建默认日志格式化模板
        var formatString = new StringBuilder();

        if (!string.IsNullOrEmpty(message))
        {
            var timeStamp = _fileLoggerProvider.UseUtcTimestamp ? DateTime.UtcNow : DateTime.Now;

            formatString.Append(timeStamp.ToString("o"));
            formatString.Append("  [");
            formatString.Append(Penetrates.GetShortLogLevel(logLevel));
            formatString.Append(']');
            formatString.Append("  [");
            formatString.Append(_logName);
            formatString.Append(']');
            formatString.Append("  [");
            formatString.Append(eventId);
            formatString.Append("]  ");
            formatString.Append(message);
        }

        // 如果包含异常信息，则创建新一行写入
        if (exception != null) formatString.AppendLine(exception.ToString());

        // 写入日志队列
        _fileLoggerProvider.WriteToQueue(formatString.ToString());
    }
}