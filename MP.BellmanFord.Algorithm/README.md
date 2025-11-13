# MP.Prims.Algorithm

## result

```bash
initial state:
   vertex | distance   | predecessor
  S       | 0          | null
  A       | infinity   | null
  B       | infinity   | null
  C       | infinity   | null
  D       | infinity   | null
  E       | infinity   | null
  F       | infinity   | null
  G       | infinity   | null
  H       | infinity   | null

iteration 1:
  - edge (S, A): checking if 0 + 5 < infinity. Distance updated
  - edge (A, C): checking if 5 + -3 < infinity. Distance updated
  - edge (A, E): checking if 5 + 2 < infinity. Distance updated
  - edge (C, B): checking if 2 + 7 < infinity. Distance updated
  - edge (C, H): checking if 2 + -3 < infinity. Distance updated
  - edge (H, D): checking if -1 + 1 < infinity. Distance updated
   vertex | distance   | predecessor
  S       | 0          | null
  A       | 5          | S
  B       | 9          | C
  C       | 2          | A
  D       | 0          | H
  E       | 7          | A
  F       | infinity   | null
  G       | infinity   | null
  H       | -1         | C

iteration 2:
  - edge (S, A): checking if 0 + 5 < 5. No update
  - edge (A, C): checking if 5 + -3 < 2. No update
  - edge (A, E): checking if 5 + 2 < 7. No update
  - edge (B, F): checking if 9 + -5 < infinity. Distance updated
  - edge (C, B): checking if 2 + 7 < 9. No update
  - edge (C, H): checking if 2 + -3 < -1. No update
  - edge (F, G): checking if 4 + 2 < infinity. Distance updated
  - edge (G, B): checking if 6 + 4 < 9. No update
  - edge (H, D): checking if -1 + 1 < 0. No update
   vertex | distance   | predecessor
  S       | 0          | null
  A       | 5          | S
  B       | 9          | C
  C       | 2          | A
  D       | 0          | H
  E       | 7          | A
  F       | 4          | B
  G       | 6          | F
  H       | -1         | C

iteration 3:
  - edge (S, A): checking if 0 + 5 < 5. No update
  - edge (A, C): checking if 5 + -3 < 2. No update
  - edge (A, E): checking if 5 + 2 < 7. No update
  - edge (B, F): checking if 9 + -5 < 4. No update
  - edge (C, B): checking if 2 + 7 < 9. No update
  - edge (C, H): checking if 2 + -3 < -1. No update
  - edge (F, G): checking if 4 + 2 < 6. No update
  - edge (G, B): checking if 6 + 4 < 9. No update
  - edge (H, D): checking if -1 + 1 < 0. No update
   vertex | distance   | predecessor
  S       | 0          | null
  A       | 5          | S
  B       | 9          | C
  C       | 2          | A
  D       | 0          | H
  E       | 7          | A
  F       | 4          | B
  G       | 6          | F
  H       | -1         | C

iteration 4:
  - edge (S, A): checking if 0 + 5 < 5. No update
  - edge (A, C): checking if 5 + -3 < 2. No update
  - edge (A, E): checking if 5 + 2 < 7. No update
  - edge (B, F): checking if 9 + -5 < 4. No update
  - edge (C, B): checking if 2 + 7 < 9. No update
  - edge (C, H): checking if 2 + -3 < -1. No update
  - edge (F, G): checking if 4 + 2 < 6. No update
  - edge (G, B): checking if 6 + 4 < 9. No update
  - edge (H, D): checking if -1 + 1 < 0. No update
   vertex | distance   | predecessor
  S       | 0          | null
  A       | 5          | S
  B       | 9          | C
  C       | 2          | A
  D       | 0          | H
  E       | 7          | A
  F       | 4          | B
  G       | 6          | F
  H       | -1         | C

iteration 5:
  - edge (S, A): checking if 0 + 5 < 5. No update
  - edge (A, C): checking if 5 + -3 < 2. No update
  - edge (A, E): checking if 5 + 2 < 7. No update
  - edge (B, F): checking if 9 + -5 < 4. No update
  - edge (C, B): checking if 2 + 7 < 9. No update
  - edge (C, H): checking if 2 + -3 < -1. No update
  - edge (F, G): checking if 4 + 2 < 6. No update
  - edge (G, B): checking if 6 + 4 < 9. No update
  - edge (H, D): checking if -1 + 1 < 0. No update
   vertex | distance   | predecessor
  S       | 0          | null
  A       | 5          | S
  B       | 9          | C
  C       | 2          | A
  D       | 0          | H
  E       | 7          | A
  F       | 4          | B
  G       | 6          | F
  H       | -1         | C

iteration 6:
  - edge (S, A): checking if 0 + 5 < 5. No update
  - edge (A, C): checking if 5 + -3 < 2. No update
  - edge (A, E): checking if 5 + 2 < 7. No update
  - edge (B, F): checking if 9 + -5 < 4. No update
  - edge (C, B): checking if 2 + 7 < 9. No update
  - edge (C, H): checking if 2 + -3 < -1. No update
  - edge (F, G): checking if 4 + 2 < 6. No update
  - edge (G, B): checking if 6 + 4 < 9. No update
  - edge (H, D): checking if -1 + 1 < 0. No update
   vertex | distance   | predecessor
  S       | 0          | null
  A       | 5          | S
  B       | 9          | C
  C       | 2          | A
  D       | 0          | H
  E       | 7          | A
  F       | 4          | B
  G       | 6          | F
  H       | -1         | C

iteration 7:
  - edge (S, A): checking if 0 + 5 < 5. No update
  - edge (A, C): checking if 5 + -3 < 2. No update
  - edge (A, E): checking if 5 + 2 < 7. No update
  - edge (B, F): checking if 9 + -5 < 4. No update
  - edge (C, B): checking if 2 + 7 < 9. No update
  - edge (C, H): checking if 2 + -3 < -1. No update
  - edge (F, G): checking if 4 + 2 < 6. No update
  - edge (G, B): checking if 6 + 4 < 9. No update
  - edge (H, D): checking if -1 + 1 < 0. No update
   vertex | distance   | predecessor
  S       | 0          | null
  A       | 5          | S
  B       | 9          | C
  C       | 2          | A
  D       | 0          | H
  E       | 7          | A
  F       | 4          | B
  G       | 6          | F
  H       | -1         | C

iteration 8:
  - edge (S, A): checking if 0 + 5 < 5. No update
  - edge (A, C): checking if 5 + -3 < 2. No update
  - edge (A, E): checking if 5 + 2 < 7. No update
  - edge (B, F): checking if 9 + -5 < 4. No update
  - edge (C, B): checking if 2 + 7 < 9. No update
  - edge (C, H): checking if 2 + -3 < -1. No update
  - edge (F, G): checking if 4 + 2 < 6. No update
  - edge (G, B): checking if 6 + 4 < 9. No update
  - edge (H, D): checking if -1 + 1 < 0. No update
   vertex | distance   | predecessor
  S       | 0          | null
  A       | 5          | S
  B       | 9          | C
  C       | 2          | A
  D       | 0          | H
  E       | 7          | A
  F       | 4          | B
  G       | 6          | F
  H       | -1         | C

finished

shortest paths from 'S':
   vertex | distance   | path
  S       | 0          | S
  A       | 5          | S -> A
  B       | 9          | S -> A -> C -> B
  C       | 2          | S -> A -> C
  D       | 0          | S -> A -> C -> H -> D
  E       | 7          | S -> A -> E
  F       | 4          | S -> A -> C -> B -> F
  G       | 6          | S -> A -> C -> B -> F -> G
  H       | -1         | S -> A -> C -> H

Process finished with exit code 0.
```
