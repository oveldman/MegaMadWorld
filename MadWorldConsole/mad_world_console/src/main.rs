mod windows;

use crate::windows::os_api;
use crate::windows::screen_api;

fn main() {
    if !os_api::is_windows() {
        println!("I'm sorry! This is not windows. Try this tool on windows.");
        return;
    }

    screen_api::scale();
}
