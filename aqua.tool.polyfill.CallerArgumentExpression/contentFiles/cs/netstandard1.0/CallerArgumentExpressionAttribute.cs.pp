﻿// <auto-generated>
//   This code file has automatically been generated by the "aqua.tool.polyfill.CallerArgumentExpression" NuGet package (https://www.nuget.org/packages/aqua.tool.polyfill.CallerArgumentExpression).
//   Copyright (c) Christof Senn. See license.txt in the package root for license information.
// </auto-generated>

#if !AQUA_TOOL_POLYFILL_CALLERARGUMENTEXPRESSION_DISABLE
#nullable enable
#pragma warning disable

namespace System.Runtime.CompilerServices
{
    using global::System.CodeDom.Compiler;
    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Indicates that a parameter captures the expression passed for another parameter as a string.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [DebuggerNonUserCode]
    [GeneratedCode("aqua.tool.polyfill.CallerArgumentExpression", "")]
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
#if AQUA_TOOL_POLYFILL_CALLERARGUMENTEXPRESSION_PUBLIC
    public
#else
    internal
#endif // AQUA_TOOL_POLYFILL_CALLERARGUMENTEXPRESSION_PUBLIC
    sealed class CallerArgumentExpressionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallerArgumentExpressionAttribute"/> class.
        /// </summary>
        /// <param name="parameterName">The name of the parameter whose expression should be captured as a string.</param>
        public CallerArgumentExpressionAttribute(string parameterName)
            => ParameterName = parameterName;
    
        /// <summary>
        /// Gets the name of the parameter whose expression should be captured as a string.
        /// </summary>
        public string ParameterName { get; }
    }
}

#pragma warning restore
#nullable restore
#endif // AQUA_TOOL_POLYFILL_CALLERARGUMENTEXPRESSION_DISABLE