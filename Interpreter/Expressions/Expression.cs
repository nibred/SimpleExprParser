﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Expressions;

internal abstract class Expression
{
    public abstract int Evaluate();
}
