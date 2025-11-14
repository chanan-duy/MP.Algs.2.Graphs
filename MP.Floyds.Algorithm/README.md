# MP.Floyds.Algorithm

## result

```bash
initial state:
distance matrix:
     a        b        c        d        e        
a    0        3        4        infinity 1        
b    3        0        5        infinity infinity 
c    4        5        0        2        6        
d    infinity infinity 2        0        7        
e    1        infinity 6        7        0        

predecessor matrix:
     a        b        c        d        e        
a    null     b        c        null     e        
b    a        null     c        null     null     
c    a        b        null     d        e        
d    null     null     c        null     e        
e    a        null     c        d        null     

step 1: looking at vertex 'a'
  - updating distance between 'b' and 'e'. New path: b -> a -> e. New distance: 4
  - updating distance between 'c' and 'e'. New path: c -> a -> e. New distance: 5
  - updating distance between 'e' and 'b'. New path: e -> a -> b. New distance: 4
  - updating distance between 'e' and 'c'. New path: e -> a -> c. New distance: 5

state after step:
distance matrix:
     a        b        c        d        e        
a    0        3        4        infinity 1        
b    3        0        5        infinity 4        
c    4        5        0        2        5        
d    infinity infinity 2        0        7        
e    1        4        5        7        0        

predecessor matrix:
     a        b        c        d        e        
a    null     b        c        null     e        
b    a        null     c        null     a        
c    a        b        null     d        a        
d    null     null     c        null     e        
e    a        a        a        d        null     

step 2: looking at vertex 'b'

state after step:
distance matrix:
     a        b        c        d        e        
a    0        3        4        infinity 1        
b    3        0        5        infinity 4        
c    4        5        0        2        5        
d    infinity infinity 2        0        7        
e    1        4        5        7        0        

predecessor matrix:
     a        b        c        d        e        
a    null     b        c        null     e        
b    a        null     c        null     a        
c    a        b        null     d        a        
d    null     null     c        null     e        
e    a        a        a        d        null     

step 3: looking at vertex 'c'
  - updating distance between 'a' and 'd'. New path: a -> c -> d. New distance: 6
  - updating distance between 'b' and 'd'. New path: b -> c -> d. New distance: 7
  - updating distance between 'd' and 'a'. New path: d -> c -> a. New distance: 6
  - updating distance between 'd' and 'b'. New path: d -> c -> b. New distance: 7

state after step:
distance matrix:
     a        b        c        d        e        
a    0        3        4        6        1        
b    3        0        5        7        4        
c    4        5        0        2        5        
d    6        7        2        0        7        
e    1        4        5        7        0        

predecessor matrix:
     a        b        c        d        e        
a    null     b        c        c        e        
b    a        null     c        c        a        
c    a        b        null     d        a        
d    c        c        c        null     e        
e    a        a        a        d        null     

step 4: looking at vertex 'd'

state after step:
distance matrix:
     a        b        c        d        e        
a    0        3        4        6        1        
b    3        0        5        7        4        
c    4        5        0        2        5        
d    6        7        2        0        7        
e    1        4        5        7        0        

predecessor matrix:
     a        b        c        d        e        
a    null     b        c        c        e        
b    a        null     c        c        a        
c    a        b        null     d        a        
d    c        c        c        null     e        
e    a        a        a        d        null     

step 5: looking at vertex 'e'

state after step:
distance matrix:
     a        b        c        d        e        
a    0        3        4        6        1        
b    3        0        5        7        4        
c    4        5        0        2        5        
d    6        7        2        0        7        
e    1        4        5        7        0        

predecessor matrix:
     a        b        c        d        e        
a    null     b        c        c        e        
b    a        null     c        c        a        
c    a        b        null     d        a        
d    c        c        c        null     e        
e    a        a        a        d        null     
finished

final shortest path distances:
     a        b        c        d        e        
a    0        3        4        6        1        
b    3        0        5        7        4        
c    4        5        0        2        5        
d    6        7        2        0        7        
e    1        4        5        7        0        

all-pairs shortest paths:
  a -> b | path: a -> b | distance: 3
  a -> c | path: a -> c | distance: 4
  a -> d | path: a -> c -> d | distance: 6
  a -> e | path: a -> e | distance: 1
  b -> a | path: b -> a | distance: 3
  b -> c | path: b -> c | distance: 5
  b -> d | path: b -> c -> d | distance: 7
  b -> e | path: b -> a -> e | distance: 4
  c -> a | path: c -> a | distance: 4
  c -> b | path: c -> b | distance: 5
  c -> d | path: c -> d | distance: 2
  c -> e | path: c -> a -> e | distance: 5
  d -> a | path: d -> c -> a | distance: 6
  d -> b | path: d -> c -> b | distance: 7
  d -> c | path: d -> c | distance: 2
  d -> e | path: d -> e | distance: 7
  e -> a | path: e -> a | distance: 1
  e -> b | path: e -> a -> b | distance: 4
  e -> c | path: e -> a -> c | distance: 5
  e -> d | path: e -> d | distance: 7

Process finished with exit code 0.
```
