# MP.Kruskals.Algorithm

## result

```bash
initial state: all edges sorted by weight
  Edge   | Weight
  a - b   | 1
  d - e   | 2
  b - e   | 3
  b - d   | 4
  b - c   | 5
  c - e   | 6
  a - c   | 7


step 1: evaluating edge a - b with weight 1
  - Find(a) -> 'a', Find(b) -> 'b'. Vertices are in different sets. Edge accepted.
  current mst edges:
    a - b (1)

step 2: evaluating edge d - e with weight 2
  - Find(d) -> 'd', Find(e) -> 'e'. Vertices are in different sets. Edge accepted.
  current mst edges:
    a - b (1)
    d - e (2)

step 3: evaluating edge b - e with weight 3
  - Find(b) -> 'a', Find(e) -> 'd'. Vertices are in different sets. Edge accepted.
  current mst edges:
    a - b (1)
    d - e (2)
    b - e (3)

step 4: evaluating edge b - d with weight 4
  - Find(b) -> 'a', Find(d) -> 'a'. Vertices are in the same set. Edge rejected (forms a cycle).

step 5: evaluating edge b - c with weight 5
  - Find(b) -> 'a', Find(c) -> 'c'. Vertices are in different sets. Edge accepted.
  current mst edges:
    a - b (1)
    d - e (2)
    b - e (3)
    b - c (5)

step 6: evaluating edge c - e with weight 6
  - Find(c) -> 'a', Find(e) -> 'a'. Vertices are in the same set. Edge rejected (forms a cycle).

step 7: evaluating edge a - c with weight 7
  - Find(a) -> 'a', Find(c) -> 'a'. Vertices are in the same set. Edge rejected (forms a cycle).

finished
final mst:
   Edge   | Weight
  a - b   | 1
  d - e   | 2
  b - e   | 3
  b - c   | 5
final mst weight: 11

Process finished with exit code 0.
```
