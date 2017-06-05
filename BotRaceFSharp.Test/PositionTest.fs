module PositionTest

open Model
open NUnit.Framework

[<Test>]
let ``Test constructor``() =
    let p = (2, 1)
    Assert.That(Row p, Is.EqualTo 2)
    Assert.That(Column p, Is.EqualTo 1)

[<Test>]
let ``Test position at``() =
    let p = (2, 1)
    let up = p |> Move Direction.Up
    let left = p |> Move Direction.Left
    let right = p |> Move Direction.Right
    let down = p |> Move Direction.Down

    // from
    Assert.That(Row p, Is.EqualTo 2)
    Assert.That(Column p, Is.EqualTo 1)

    // up
    Assert.That(Row up, Is.EqualTo 1, "up row" )
    Assert.That(Column up, Is.EqualTo 1, "up col")

    // left
    Assert.That(Row left, Is.EqualTo 2, "left row")
    Assert.That(Column left, Is.EqualTo 0, "left col")

    // right
    Assert.That(Row right, Is.EqualTo 2, "right row")
    Assert.That(Column right, Is.EqualTo 2, "right col")

    // down
    Assert.That(Row down, Is.EqualTo 3, "down row")
    Assert.That(Column down, Is.EqualTo 1, "down col")