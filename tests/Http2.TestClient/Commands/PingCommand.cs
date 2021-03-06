﻿// Copyright © Microsoft Open Technologies, Inc.
// All Rights Reserved       
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0

// THIS CODE IS PROVIDED ON AN *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT.

// See the Apache 2 License for the specific language governing permissions and limitations under the License.
using System;

namespace Http2.TestClient.Commands
{
    internal sealed class PingCommand : Command
    {
        public Uri Uri { get; private set; }

        internal override CommandType GetCmdType()
        {
            return CommandType.Ping;
        }

        internal override void Parse(string[] cmdArgs)
        {
            Uri uri;
            if (!Uri.TryCreate(cmdArgs[0], UriKind.Absolute, out uri))
            {
                throw new InvalidOperationException("Invalid ping format!");
            }
            Uri = uri;
        }
    }
}
