var encode_key = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+-$'
var seq = 0
var hash_key = "16fe2f1d7e0ec96df3a3c1dea6fbe3ca"
var duration = 154
var version = '1.3.4'
var hex_table = []
for (var i = 0; i < 256; i++)
    hex_table[i] = (i + 256).toString(16).substr(1);


var rnd_table = [132, 128, 227, 40, 74, 92, 73, 176, 129, 213, 154, 24, 232, 110, 11, 220]
// for (var e, i = 0; i < 16; i++) {
//     if (i & 3)
//         e = 4294967296 * Math.random()
//     rnd_table[i] = e >>> ((3 & i) << 3) & 255
// }
// rnd_table[6] = 15 & rnd_table[6] | 64
// rnd_table[63] = 63 & rnd_table[8] | 128

function uuid() {

    var i = 0
    var ret = hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + "-" +
        hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + "-" +
        hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + "-" +
        hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + "-" +
        hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]] + hex_table[rnd_table[i++]]

    ret = ret + '-' + seq++
    return ret
}

function orientation(uuid) {
    let splited = uuid.split('-')
    let seq = parseInt(splited[splited.length-1])
    let e_val = seq == 0 ? 999 : 444
    return { a: { a: e_val, b: e_val, c: e_val }, b: { a: e_val, b: e_val, c: e_val } }
}

function device_motion(uuid) {
    let splited = uuid.split('-')
    let seq = parseInt(splited[splited.length-1])
    let e_val = seq == 0 ? 999 : 444
    return { a: { a: { a: e_val, b: e_val, c: e_val }, b: { a: e_val, b: e_val, c: e_val } }, b: { a: { a: e_val, b: e_val, c: e_val }, b: { a: e_val, b: e_val, c: e_val } } }
}

function keyboard_action(id, pw) {

    console.log(id, pw)

    function stroke_log(data, secure) {

        var elapsed = 0
        var ret = []

        for (var i = 0; i < data.length; i++) {

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

        var shift = false
        var ret = { a: [], b: 0 }
        for (var i = 0; i < actions.length - 1; i++) {

            if (actions[i].c == 'SHIFT' && actions[i].b == 'd') {
                shift = true
                continue
            } else if (actions[i].c == 'SHIFT' && actions[i].b == 'u') {

                shift = false
                continue
            } else if (actions[i].b == 'u') {

                continue
            } else {

                var ch = String.fromCharCode(parseInt(actions[i].d))
                if (shift == false)
                    ch = ch.toLowerCase()
                var text = ((i == 0 ? '' : ret.a[ret.a.length - 1].c) + ch)

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

    var secure = false
    var stroke = stroke_log(id, secure)
    var itv = interval_log(stroke, secure)
    itv.a = itv.a.map(function (e) {
        return e.a + ',' + e.c
    })

    var id_data = {
        a: stroke.map(function (e) { return e.a + ',' + e.b + ',' + e.c + ',' + e.d }),
        b: itv,
        c: '',
        d: id,
        e: secure,
        f: false,
        i: 'id',
    }

    secure = true
    stroke = stroke_log(pw, true)
    itv = interval_log(stroke, secure)
    itv.a = itv.a.map(function (e) {
        return e.a + ',' + e.c
    })

    var pw_data = {
        a: stroke.map(function (e) { return e.a + ',' + e.b + ',' + e.c + ',' + e.d }),
        b: itv,
        c: '',
        d: '',
        e: true,
        f: false,
        i: 'pw',
    }

    var ret = [id_data, pw_data]
    return ret
}

function mouse_action() {

    var cursor_x = Math.floor(Math.random() * 1920)
    var cursor_y = Math.floor(Math.random() * 1080)
    var elapsed = Math.floor(Math.random() * 1500)
    var action = {
        a: 0, // mouse move type
        b: elapsed,
        c: cursor_x,
        d: cursor_y,
    }

    var ret = {
        a: ['' + action.a + '|' + action.b + '|' + action.c + '|' + action.d],
        b: 0, // mouse action count - 1
        c: cursor_x,
        d: cursor_y,
        e: elapsed,
        f: 0,
    }

    return ret
}

function get_info(id, pw) {

    var data = {
        a: uuid(),
        b: "1.3.4",
        c: false,
        d: keyboard_action(id, pw),
        e: orientation(id),
        f: device_motion(id),
        g: mouse_action(),
        j: duration,
        h: hash_key,
        i: { a: "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36", b: "ko-KR", c: 24, d: 8, e: 1, f: 4, g: [1920, 1080], h: [1920, 1050], i: -540, j: 1, k: 1, l: 1, z: 1, m: "unknown", n: "Win32", o: "unknown", aa: ["Chrome PDF Plugin::Portable Document Format::application/x-google-chrome-pdf~pdf", "Chrome PDF Viewer::::application/pdf~pdf", "Native Client::::application/x-nacl~, application/x-pnacl~"], s: false, t: false, u: false, v: false, w: false, x: [0, false, false], y: ["Arial", "Arial Black", "Arial Narrow", "Book Antiqua", "Bookman Old Style", "Calibri", "Cambria", "Cambria Math", "Century", "Comic Sans MS", "Consolas", "Courier", "Courier New", "Garamond", "Georgia", "Helvetica", "Impact", "Lucida Console", "Lucida Sans Unicode", "Microsoft Sans Serif", "Monotype Corsiva", "MS Gothic", "MS PGothic", "MS Reference Sans Serif", "MS Sans Serif", "MS Serif", "Palatino Linotype", "Segoe Print", "Segoe Script", "Segoe UI", "Segoe UI Light", "Segoe UI Semibold", "Segoe UI Symbol", "Tahoma", "Times", "Times New Roman", "Trebuchet MS", "Verdana", "Wingdings", "Wingdings 2", "Wingdings 3"] }
    }

    return data
}

function compress(e, t, n) {
    if (null == e) return "";
    var r, o, i, a = {},
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

    var data = get_info(id, pw)
    console.log(data)
    data = JSON.stringify(data)
    data = compresscompressToEncodedURIComponent(data)
    data = {uuid: id, encData: data}
    data = JSON.stringify(data)
    data = encodeURIComponent(data) // POST 방식으로 데이터를 전달할 때 기본적으로 URL Encoding 되므로

    return data
}

bvsd('boyism', 'tmdgus12!@A')