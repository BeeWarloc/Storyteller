var gulp = require('gulp');

gulp.task('default', []);

gulp.task('ci', []);

var args = require('yargs').argv,
    assemblyInfo = require('gulp-dotnet-assembly-info');


gulp.task('assemblyInfo', function() {
	// TODO -- get the build number from CI if it exists

	var buildVersion = '3.0.0';

	// TODO -- get the git commit version in here

    return gulp
        .src('src/CommonAssemblyInfo.cs')
        .pipe(assemblyInfo({
            version: buildVersion,
            fileVersion: buildVersion,
        }))
        .pipe(gulp.dest('src'));
});

var msbuild = require('gulp-msbuild');

gulp.task('build', [], function() {
    return gulp
        .src('**/*.sln')
        .pipe(msbuild({
            toolsVersion: 12.0,
            targets: ['Clean', 'Build'],
            errorOnFail: true,
            stdout: true
        }));
});