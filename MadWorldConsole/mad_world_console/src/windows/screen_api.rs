extern crate enigo;

use enigo::*;
use std::{thread, time};

pub fn scale(scale_up: bool, scale_total_options: i32) {
    let mut enigo = Enigo::new();

    open_display_settings(&mut enigo);
    open_select_settings_box(&mut enigo);
    set_scale_settings(&mut enigo, scale_up, scale_total_options);
    close_settings(&mut enigo);
}

fn open_display_settings(keyboard: &mut Enigo) {
    press_key(keyboard, Key::LWin);
    thread::sleep(time::Duration::from_millis(200));
    type_word(keyboard, String::from("display"));
    thread::sleep(time::Duration::from_millis(200));
    press_key(keyboard, Key::Return);
    thread::sleep(time::Duration::from_millis(1000));
}

fn open_select_settings_box(keyboard: &mut Enigo){
    keyboard.key_down(Key::LShift);
    press_key(keyboard, Key::Tab);
    press_key(keyboard, Key::Tab);
    keyboard.key_up(Key::LShift);
    thread::sleep(time::Duration::from_millis(200));
    press_key(keyboard, Key::Return);
}

fn set_scale_settings(keyboard: &mut Enigo, scale_up: bool, scale_total_options: i32){
    let move_key: Key = if scale_up { Key::DownArrow } else { Key::UpArrow };

    thread::sleep(time::Duration::from_millis(100));

    let mut i = 0;
    while (i < scale_total_options)
    {
        press_key(keyboard, move_key);
        i = i + 1;
    }

    press_key(keyboard, Key::Return);
    thread::sleep(time::Duration::from_millis(100));
}

fn close_settings(keyboard: &mut Enigo){
    keyboard.key_down(Key::Alt);
    press_key(keyboard, Key::F4);
    keyboard.key_up(Key::Alt);
}

fn type_word(keyboard: &mut Enigo, word: String) {
    for char in word.chars() {
        press_key(keyboard, Key::Layout(char))
    }
}

fn press_key(keyboard: &mut Enigo, key: Key) {
    keyboard.key_down(key);
    keyboard.key_up(key);
}
