#region License

//     A simplistic Behavior Tree implementation in C#
//     Copyright (C) 2010  ApocDev apocdev@gmail.com
// 
//     This file is part of TreeSharp.
// 
//     TreeSharp is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     TreeSharp is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with Foobar.  If not, see <http://www.gnu.org/licenses/>.

#endregion

namespace TreeSharp
{
    /// <summary>
    ///   Values that can be returned from composites and the like.
    /// </summary>
    public enum RunStatus
    {
        Success,
        Failure,
        Running,
    }
}