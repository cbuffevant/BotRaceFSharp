module MazeGenerator

open Model
open Model.Cell

//module DepthFirstSearch = 
//    let private rng = new System.Random()
//    type 'a stack =
//        | EmptyStack
//        | StackNode of 'a * 'a stack

//    let processNode currentCell size stack =
//        if (size > 0) then
//            let ix = rng. Next(0, size*size)
//            let newStack = StackNode(ix, stack)



//    let Create size =
//        let unvisitedCells = [| for _ in 1..size * size -> 0 |]
//        processNode 0 size (StackNode(0, EmptyStack))
        


module RecursiveBacktracking =
    let private rng = new System.Random()
    let private Shuffle (org: _[]) =
        let arr = Array.copy org
        let max = arr.Length - 1
        let randomSwap (arr : _[]) i =
            let pos = rng.Next(max)
            let tmp = arr.[pos]
            arr.[pos] <- arr.[i]
            arr.[i] <- tmp
            arr
        [|0..max|] |> Array.fold randomSwap arr 

    let rec private CarvePassageFrom p maze =
        [| Direction.Up; Direction.Down; Direction.Left; Direction.Right |] 
        |> Shuffle
        |> Array.iter (fun direction -> 
                        let newPosition = p |> Move direction
                        match maze |> Maze.CellAt newPosition with
                            | Some cell when cell |> IsClosed -> 
                                maze |> Maze.Carve p direction
                                maze |> CarvePassageFrom newPosition
                            | _ -> ())

    let Create size =
        let maze = Maze.CreateClosed size
        maze |> CarvePassageFrom (0,0)
        maze

    
