module TestDirection

open Model
open NUnit.Framework

[<Test>]
let ``Oposite direction is correct``() =
    CollectionAssert.AreEqual(
        [Direction.Up; Direction.Down; Direction.Left; Direction.Right] 
        |> List.map Oposite,
        [Direction.Down; Direction.Up; Direction.Right; Direction.Left])