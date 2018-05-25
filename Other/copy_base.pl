use strict;

my $custom_web_ui = "CSBooster.WebUI-4.0.0.0";

#########################################################################
# Get parameters
#########################################################################
my %params = ();
foreach my $arg (@ARGV)
{
    my @param = split(/=/, $arg);
    if($#param = 2)
    {
        $param[1] =~ s/\\*$//g;
        $params{$param[0]} = $param[1];
    }
}

my $web_ui_folder = $params{'--solution_dir'}."\\".$custom_web_ui;
my $web_ui_bin_folder = $web_ui_folder."\\bin";
my $project_dll = $params{'--target_path'};
(my $project_pdb = $params{'--target_path'}) =~ s/\.dll$/\.pdb/;
my $project_path = $params{'--project_dir'};

#########################################################################
# Copy content
#########################################################################
print "CMD: robocopy /R:1 /W:1 $project_path $web_ui_folder *.as?x *.js *.jpg *.png *.cur *.xml *.master *.css *.swf *.lang /s\n";
`robocopy /R:1 /W:1 "$project_path" "$web_ui_folder" *.as?x *.js *.jpg *.png *.cur *.xml *.master *.css *.swf *.lang /s`;

#########################################################################
# Copy binaries
#########################################################################
print "CMD: copy /y $project_dll $web_ui_bin_folder\n";
`copy /y "$project_dll" "$web_ui_bin_folder"`;

print "CMD: copy /y $project_pdb $web_ui_bin_folder\n";
`copy /y "$project_pdb" "$web_ui_bin_folder"`;
