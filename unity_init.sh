# print_thiss the input
function behnam_init_unity_project() {
Run git init
Run git config --global push.default current
print_this "Enter repo url: "
read url
Run git remote add origin $url
Run create_ignore
Run push "First Commit"

Run git flow init -d
Run git lfs install
Run create_attribute
Run push "Init LFS and Flow"
Run create_mergetool
}

function push()
{
Run git add .
Run git commit -m $1
Run git push
}

print_this()
{
    RED='\033[0;31m'
    NC='\033[0m' # No Color
    echo -e "${RED}$1${NC}"
}

function Run()
{
    print_this "#### $1 $2 $3 $4 $5 $6 ####"
    if $1 $2 $3 $4 $5 $6 ; then
     print_this "Done!"
    else
        print_this "Failed!"
        set -e exit
    fi
}

function create_ignore(){
echo "# =============== #
# Unity generated #
# =============== #
[Tt]emp/
[Oo]bj/
[Bb]uild
/[Bb]uilds/
/[Ll]ibrary/
sysinfo.txt
*.stackdump
/Assets/AssetStoreTools*
*.apk
*.unitypackage
 
# === #
# IDE #
# === #
/.idea/
[Ee]xported[Oo]bj/
.vs/
/*.userprefs
/*.csproj
/*.pidb
*.pidb.meta
/*.suo
/*.sln*
/*.user
/*.unityproj
/*.booproj
.consulo/
/*.tmp
/*.svd
[Aa]ssets/Plugins/Editor/JetBrains*

# ============ #
# OS generated #
# ============ #
.DS_Store*
._*
.Spotlight-V100
.Trashes
Icon?
ehthumbs.db
[Tt]humbs.db
[Dd]esktop.ini
Corridor/Library/ShaderCache/
Corridor/Library/metadata/

# ============================ #
# Never ignore Asset meta data #
# ============================ #
!/[Aa]ssets/**/*.meta

# ================== #
# TextMesh Pro files #
# ================== #
[Aa]ssets/TextMesh*Pro/

# ====== #
# Builds #
# ====== #
*.apk
*.unitypackage
*.symbols.zip
" >> .gitignore
}

function create_attribute(){
echo "# 3D models
*.3dm filter=lfs diff=lfs merge=lfs -text
*.3ds filter=lfs diff=lfs merge=lfs -text
*.blend filter=lfs diff=lfs merge=lfs -text
*.c4d filter=lfs diff=lfs merge=lfs -text
*.collada filter=lfs diff=lfs merge=lfs -text
*.dae filter=lfs diff=lfs merge=lfs -text
*.dxf filter=lfs diff=lfs merge=lfs -text
*.fbx filter=lfs diff=lfs merge=lfs -text
*.jas filter=lfs diff=lfs merge=lfs -text
*.lws filter=lfs diff=lfs merge=lfs -text
*.lxo filter=lfs diff=lfs merge=lfs -text
*.ma filter=lfs diff=lfs merge=lfs -text
*.max filter=lfs diff=lfs merge=lfs -text
*.mb filter=lfs diff=lfs merge=lfs -text
*.obj filter=lfs diff=lfs merge=lfs -text
*.ply filter=lfs diff=lfs merge=lfs -text
*.skp filter=lfs diff=lfs merge=lfs -text
*.stl filter=lfs diff=lfs merge=lfs -text
*.ztl filter=lfs diff=lfs merge=lfs -text
# Audio
*.aif filter=lfs diff=lfs merge=lfs -text
*.aiff filter=lfs diff=lfs merge=lfs -text
*.it filter=lfs diff=lfs merge=lfs -text
*.mod filter=lfs diff=lfs merge=lfs -text
*.mp3 filter=lfs diff=lfs merge=lfs -text
*.ogg filter=lfs diff=lfs merge=lfs -text
*.s3m filter=lfs diff=lfs merge=lfs -text
*.wav filter=lfs diff=lfs merge=lfs -text
*.xm filter=lfs diff=lfs merge=lfs -text
# Fonts
*.otf filter=lfs diff=lfs merge=lfs -text
*.ttf filter=lfs diff=lfs merge=lfs -text
# Images
*.bmp filter=lfs diff=lfs merge=lfs -text
*.exr filter=lfs diff=lfs merge=lfs -text
*.gif filter=lfs diff=lfs merge=lfs -text
*.hdr filter=lfs diff=lfs merge=lfs -text
*.iff filter=lfs diff=lfs merge=lfs -text
*.jpeg filter=lfs diff=lfs merge=lfs -text
*.jpg filter=lfs diff=lfs merge=lfs -text
*.pict filter=lfs diff=lfs merge=lfs -text
*.png filter=lfs diff=lfs merge=lfs -text
*.psd filter=lfs diff=lfs merge=lfs -text
*.tga filter=lfs diff=lfs merge=lfs -text
*.tif filter=lfs diff=lfs merge=lfs -text
*.tiff filter=lfs diff=lfs merge=lfs -text" >> .gitattributes
}

function create_mergetool(){
echo "[merge]
    tool = unityyamlmerge
[mergetool \"unityyamlmerge\"]
    trustExitCode = false
    cmd = '/Applications/Unity2019.3.6f1/Unity2019.3.6f1.app/Contents/Tools/UnityYAMLMerge' merge -p \"\$BASE\" \"\$REMOTE\" \"\$LOCAL\" \"\$MERGED\"" >> .git/config
}

behnam_init_unity_project