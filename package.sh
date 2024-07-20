read -p "Enter version number: " version
read -p "Enter message: " message

git tag -a $version -m "$message"
git push origin $version

rm -rf bin
mkdir bin

cd PWManager.CLI
rm -rf bin/Release

dotnet restore

dotnet build --runtime linux-x64 --configuration Release
dotnet build --runtime win-x64 --configuration Release

cd bin/Release/net8.0
tar -czvf cli-linux-x64.tar.gz linux-x64
tar -czvf cli-win-x64.tar.gz win-x64

sha256sum cli-linux-x64.tar.gz >> checksums.txt
sha256sum cli-win-x64.tar.gz >> checksums.txt

rm -rf win-x64
rm -rf linux-x64

mv * ../../../../bin