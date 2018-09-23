rm -rf Temp/

res=$(fuser Temp/$(ls -a Temp/ | grep ".fuse"))
array=$(echo $res | tr " " "\n")

for x in $array
do
    kill -9 $x
done
