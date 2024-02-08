mod windows;

use std::env;

use crate::windows::os_api;
use crate::windows::screen_api;

fn main() {
    if !os_api::is_windows() {
        println!("I'm sorry! This is not windows. Try this tool on windows.");
        return;
    }

    let args: Vec<String> = env::args().collect();
    let mut scale_up: bool = false;
    let mut scale_total_options: i32 = 0;

    if (args.len() > 1){
        scale_total_options = args[1].parse::<i32>().unwrap();
    }

    if (args.len() > 2){
        scale_up = args[2] == "up";
    }


    screen_api::scale(scale_up, scale_total_options);
}
