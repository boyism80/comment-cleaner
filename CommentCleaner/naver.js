const encode_key = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+-$'
let seq = 0
const hash_key = "c5ee9e9f21e7802d466d142e13615e5e"
const duration = 69
const version = '1.3.4'
let hex_table = []
for (let i = 0; i < 256; i++)
    hex_table[i] = (i + 256).toString(16).substr(1);

// let rnd_table = [45, 211, 61, 91, 113, 244, 56, 154, 140, 89, 142, 48, 185, 211, 140, 173]
let rnd_table = []
for (let e, i = 0; i < 16; i++) {
    if (i & 3)
        e = 4294967296 * Math.random()
    rnd_table[i] = e >>> ((3 & i) << 3) & 255
}
rnd_table[6] = 15 & rnd_table[6] | 64
rnd_table[8] = 63 & rnd_table[8] | 128

function uuid(seq) {

    let i = 0
    let ret = hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + "-" +
        hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + "-" +
        hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + "-" +
        hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + "-" +
        hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]]

    ret = ret + '-' + seq
    return ret
}

function orientation(uuid, seq) {
    const e_val = seq == 0 ? 999 : 444
    return { a: { a: e_val, b: e_val, c: e_val }, b: { a: e_val, b: e_val, c: e_val } }
}

function device_motion(uuid, seq) {
    const e_val = seq == 0 ? 999 : 444
    return { a: { a: { a: e_val, b: e_val, c: e_val }, b: { a: e_val, b: e_val, c: e_val } }, b: { a: { a: e_val, b: e_val, c: e_val }, b: { a: e_val, b: e_val, c: e_val } } }
}

function keyboard_action(id, pw) {

    function stroke_log(data, secure) {

        function require_shift(data) {

            if(data.toUpperCase() == data)
                return true

            const format = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/;
            if(format.test(data))
                return true

            return false
        }

        let elapsed = 0
        let ret = []
        let prev = null

        for (let i = 0; i < data.length; i++) {

            ret.push({
                a: elapsed,
                b: 'd',
                c: 'i' + i,
                d: secure ? '' : '' + data.toUpperCase().charCodeAt(i)
            })
            elapsed = Math.floor(100 + Math.random() * 150)

            ret.push({
                a: elapsed,
                b: 'u',
                c: 'i' + i,
                d: secure ? '' : '' + data.toUpperCase().charCodeAt(i)
            })
            elapsed = Math.floor(100 + Math.random() * 150)
        }

        ret.push({
            a: elapsed,
            b: 'd',
            c: secure ? 'ENTER' : 'TAB',
            d: secure ? '' : '9'
        })

        return ret
    }

    function interval_log(actions, secure) {

        let shift = false
        let ret = { a: [], b: 0 }
        for (let i = 0; i < actions.length - 1; i++) {

            if (actions[i].c == 'SHIFT' && actions[i].b == 'd') {
                shift = true
                continue
            } else if (actions[i].c == 'SHIFT' && actions[i].b == 'u') {

                shift = false
                continue
            } else if (actions[i].b == 'u') {

                continue
            } else {

                let ch = String.fromCharCode(parseInt(actions[i].d))
                if (shift == false)
                    ch = ch.toLowerCase()
                let text = ((i == 0 ? '' : ret.a[ret.a.length - 1].c) + ch)

                ret.a.push({
                    a: actions[i].a,
                    c: secure ? '' : text,
                    d: i,
                    e: secure ? -1 : text.length,
                })
            }
        }

        ret.b = ret.a.length - 1
        return ret
    }

    let secure = false
    let stroke = stroke_log(id, secure)
    let itv = interval_log(stroke, secure)
    itv.a = itv.a.map(function (e) {
        return e.a + ',' + e.c
    })

    let id_data = {
        a: stroke.map(function (e) { return e.a + ',' + e.b + ',' + e.c + ',' + e.d }),
        b: itv,
        c: '',
        d: id,
        e: false,
        f: false,
        i: 'id',
    }

    secure = true
    stroke = stroke_log(pw, true)
    itv = interval_log(stroke, secure)
    itv.a = itv.a.map(function (e) {
        return e.a + ',' + e.c
    })

    let pw_data = {
        a: stroke.map(function (e) { return e.a + ',' + e.b + ',' + e.c + ',' + e.d }),
        b: itv,
        c: '',
        d: '',
        e: true,
        f: false,
        i: 'pw',
    }

    let ret = [id_data, pw_data]
    return ret
}

function mouse_action() {

    // let cursor_x = Math.floor(Math.random() * 1920)
    // let cursor_y = Math.floor(Math.random() * 1080)
    // let elapsed = Math.floor(Math.random() * 1500)
    // let action = {
    //     a: 0, // mouse move type
    //     b: elapsed,
    //     c: cursor_x,
    //     d: cursor_y,
    // }

    // let ret = {
    //     a: ['' + action.a + '|' + action.b + '|' + action.c + '|' + action.d],
    //     b: 0, // mouse action count - 1
    //     c: cursor_x,
    //     d: cursor_y,
    //     e: elapsed,
    //     f: 0,
    // }

    // return ret

    return {a: [], b: 0, c: 0, d: 0, e: -1, f: 0}
}

function get_info(id, pw) {

    let data = {
        a: uuid(seq),
        b: "1.3.4",
        c: false,
        d: keyboard_action(id, pw),
        e: orientation(id, seq),
        f: device_motion(id, seq),
        g: mouse_action(),
        j: duration,
        h: hash_key,
        i: {"a":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36","b":"ko-KR","c":24,"d":8,"e":1,"f":12,"g":[1920,1080],"h":[1920,1050],"i":-540,"j":1,"k":1,"l":1,"z":1,"m":"unknown","n":"Win32","o":"unknown","aa":["Chrome PDF Plugin::Portable Document Format::application/x-google-chrome-pdf~pdf","Chrome PDF Viewer::::application/pdf~pdf","Native Client::::application/x-nacl~,application/x-pnacl~"],"p":"65f2b01be6b8657dc6e417b6e44a2b85","q":"880197147088b98564c2112e05a416c7","r":"Google Inc.~ANGLE (NVIDIA GeForce GTX 1080 Direct3D11 vs_5_0 ps_5_0)","s":false,"t":false,"u":false,"v":false,"w":false,"x":[0,false,false],"y":["Arial","Arial Black","Arial Narrow","Book Antiqua","Bookman Old Style","Calibri","Cambria","Cambria Math","Century","Century Gothic","Century Schoolbook","Comic Sans MS","Consolas","Courier","Courier New","Garamond","Georgia","Helvetica","Impact","Lucida Bright","Lucida Calligraphy","Lucida Console","Lucida Fax","Lucida Handwriting","Lucida Sans","Lucida Sans Typewriter","Lucida Sans Unicode","Microsoft Sans Serif","Monotype Corsiva","MS Gothic","MS PGothic","MS Reference Sans Serif","MS Sans Serif","MS Serif","Palatino Linotype","Segoe Print","Segoe Script","Segoe UI","Segoe UI Light","Segoe UI Semibold","Segoe UI Symbol","Tahoma","Times","Times New Roman","Trebuchet MS","Verdana","Wingdings","Wingdings 2","Wingdings 3"]}
    }

    seq++

    return data
}

function compress(e, t, n) {
    if (null == e) return "";
    let r, o, i, a = {},
        s = {},
        u = "",
        c = "",
        l = "",
        f = 2,
        d = 3,
        h = 2,
        p = [],
        g = 0,
        _ = 0;
    for (i = 0; i < e.length; i += 1)
        if (u = e.charAt(i), Object.prototype.hasOwnProperty.call(a, u) || (a[u] = d++ , s[u] = !0), c = l + u, Object.prototype.hasOwnProperty.call(a, c)) l = c;
        else {
            if (Object.prototype.hasOwnProperty.call(s, l)) {
                if (l.charCodeAt(0) < 256) {
                    for (r = 0; r < h; r++) g <<= 1, _ == t - 1 ? (_ = 0, p.push(n(g)), g = 0) : _++;
                    for (o = l.charCodeAt(0), r = 0; r < 8; r++) g = g << 1 | 1 & o, _ == t - 1 ? (_ = 0, p.push(n(g)), g = 0) : _++ , o >>= 1
                } else {
                    for (o = 1, r = 0; r < h; r++) g = g << 1 | o, _ == t - 1 ? (_ = 0, p.push(n(g)), g = 0) : _++ , o = 0;
                    for (o = l.charCodeAt(0), r = 0; r < 16; r++) g = g << 1 | 1 & o, _ == t - 1 ? (_ = 0, p.push(n(g)), g = 0) : _++ , o >>= 1
                }
                f-- , 0 == f && (f = Math.pow(2, h), h++), delete s[l]
            } else
                for (o = a[l], r = 0; r < h; r++) g = g << 1 | 1 & o, _ == t - 1 ? (_ = 0, p.push(n(g)), g = 0) : _++ , o >>= 1;
            f-- , 0 == f && (f = Math.pow(2, h), h++), a[c] = d++ , l = String(u)
        }
    if ("" !== l) {
        if (Object.prototype.hasOwnProperty.call(s, l)) {
            if (l.charCodeAt(0) < 256) {
                for (r = 0; r < h; r++) g <<= 1, _ == t - 1 ? (_ = 0, p.push(n(g)), g = 0) : _++;
                for (o = l.charCodeAt(0), r = 0; r < 8; r++) g = g << 1 | 1 & o, _ == t - 1 ? (_ = 0, p.push(n(g)), g = 0) : _++ , o >>= 1
            } else {
                for (o = 1, r = 0; r < h; r++) g = g << 1 | o, _ == t - 1 ? (_ = 0, p.push(n(g)), g = 0) : _++ , o = 0;
                for (o = l.charCodeAt(0), r = 0; r < 16; r++) g = g << 1 | 1 & o, _ == t - 1 ? (_ = 0, p.push(n(g)), g = 0) : _++ , o >>= 1
            }
            f-- , 0 == f && (f = Math.pow(2, h), h++), delete s[l]
        } else
            for (o = a[l], r = 0; r < h; r++) g = g << 1 | 1 & o, _ == t - 1 ? (_ = 0, p.push(n(g)), g = 0) : _++ , o >>= 1;
        f-- , 0 == f && (f = Math.pow(2, h), h++)
    }
    for (o = 2, r = 0; r < h; r++) g = g << 1 | 1 & o, _ == t - 1 ? (_ = 0, p.push(n(g)), g = 0) : _++ , o >>= 1;
    for (; ;) {
        if (g <<= 1, _ == t - 1) {
            p.push(n(g));
            break
        }
        _++
    }
    return p.join("")
}

function compresscompressToEncodedURIComponent(data) {

    if (data == null)
        return ''

    return compress(data, 6, function (i) {
        return encode_key.charAt(i)
    })
}

function bvsd(id, pw) {

    let data = get_info(id, pw)
    const uuid = data.a
    data = JSON.stringify(data)
    data = compresscompressToEncodedURIComponent(data)
    data = {uuid: uuid, encData: data}
    data = JSON.stringify(data)
    data = encodeURIComponent(data) // POST 방식으로 데이터를 전달할 때 기본적으로 URL Encoding 되므로

    return data
}