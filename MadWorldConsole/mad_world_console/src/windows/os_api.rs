use std::env;

pub fn is_windows() -> bool {
    env::consts::OS == "windows"
}