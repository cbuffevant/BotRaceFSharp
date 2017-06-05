module MazeTest

open System.Text
open Model
open MazeHelper
open NUnit.Framework

[<Test>]
let ``Test closed draw``() =
    let expected = 
        (new StringBuilder())
            .AppendLine(" _____")
            .AppendLine("|_|_|_|")
            .AppendLine("|_|_|_|")
            .AppendLine("|_|_|_|")
            .ToString() 
    Assert.That(
        (Maze.CreateClosed 3) |> Draw, 
        Is.EqualTo expected)
 