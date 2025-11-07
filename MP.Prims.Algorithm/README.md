# MP.Prims.Algorithm

## result

```bash
initial state:
   Vertex | In MST | Key      | Parent
  a       | false  | 0        | null
  b       | false  | infinity | null
  c       | false  | infinity | null
  d       | false  | infinity | null
  e       | false  | infinity | null

step 1: select Vertex 'a'
  - neighbor 'b': edge weight (3) vs current key (infinity). Key updated
  - neighbor 'c': edge weight (4) vs current key (infinity). Key updated
  - neighbor 'e': edge weight (1) vs current key (infinity). Key updated

state after step:
   Vertex | In MST | Key      | Parent
  a       | true   | 0        | null
  b       | false  | 3        | a
  c       | false  | 4        | a
  d       | false  | infinity | null
  e       | false  | 1        | a

step 2: select Vertex 'e'
  - neighbor 'c': edge weight (6) vs current key (4). No update
  - neighbor 'd': edge weight (7) vs current key (infinity). Key updated

state after step:
   Vertex | In MST | Key      | Parent
  a       | true   | 0        | null
  b       | false  | 3        | a
  c       | false  | 4        | a
  d       | false  | 7        | e
  e       | true   | 1        | a

step 3: select Vertex 'b'
  - neighbor 'c': edge weight (5) vs current key (4). No update

state after step:
   Vertex | In MST | Key      | Parent
  a       | true   | 0        | null
  b       | true   | 3        | a
  c       | false  | 4        | a
  d       | false  | 7        | e
  e       | true   | 1        | a

step 4: select Vertex 'c'
  - neighbor 'd': edge weight (2) vs current key (7). Key updated

state after step:
   Vertex | In MST | Key      | Parent
  a       | true   | 0        | null
  b       | true   | 3        | a
  c       | true   | 4        | a
  d       | false  | 2        | c
  e       | true   | 1        | a

step 5: select Vertex 'd'

state after step:
   Vertex | In MST | Key      | Parent
  a       | true   | 0        | null
  b       | true   | 3        | a
  c       | true   | 4        | a
  d       | true   | 2        | c
  e       | true   | 1        | a
finished
final mst:
   Edge   | Weight
  a - b   | 3
  a - c   | 4
  c - d   | 2
  a - e   | 1
final mst weight: 10

Process finished with exit code 0.
```
