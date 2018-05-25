$devel_folder   = "$ARGV[0]";
$publish_folder = "$ARGV[1]";

if($devel_folder eq "")
{
    print "*** Error: Development WebUI folder missing\n\n";
    exit;
}
if($publish_folder eq "")
{
    print "*** Error: Publish WebUI folder missing\n\n";
    exit;
}

print "\nCopy files\n";
print "CMD: robocopy /R:1 /W:1 $devel_folder $publish_folder *.as?x *.js *.jpg *.png *.cur *.xml *.master *.css *.swf *.lang *.tdf /s\n";
`robocopy /R:1 /W:1 "$devel_folder" "$publish_folder" *.as?x *.js *.jpg *.png *.cur *.xml *.master *.css *.swf *.lang *.tdf /s`;

print "\nRemove pdb's\n";
print "------------\n";
print "CMD: del /q $publish_folder\\Bin\\*.pdb\n";
`del /q "$publish_folder\\Bin\\*.pdb"`;
