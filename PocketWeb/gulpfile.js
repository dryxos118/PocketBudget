const gulp = require("gulp");
const sass = require("gulp-sass")(require("sass"));
const concat = require("gulp-concat");

const paths = {
    scss: "Assets/**/*.scss",
    cssDest: "wwwroot",
};

function buildScss() {
    return gulp.src(paths.scss)
        .pipe(sass({ outputStyle: "compressed" }).on("error", sass.logError))
        .pipe(concat("pocket-budget.css"))
        .pipe(gulp.dest(paths.cssDest));
}

function watchFiles() {
    gulp.watch(paths.scss, buildScss);
}

exports.build = buildScss;
exports.watch = gulp.series(buildScss, watchFiles);
exports.default = exports.watch;