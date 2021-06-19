
fileName = "test.bin"

l = list()
for i in range(256):
    l.append(i)

arr = bytearray(l)
b_arr = bytes(arr)

with open(fileName, "wb") as f:
    f.write(b_arr)



