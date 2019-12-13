# Comment cleaner

네이버 댓글을 한번에 지우려고 개발한 어플리케이션입니다.
목적은 댓글을 한번에 지우는 것으로 시작했지만 개발 중 자동도배 기능을 추가할 수 있을 것 같아 추가했습니다.
개발언어는 C#, 프레임워크는 WPF를 이용했고 bvsd를 생성하는 부분은 javascript로 구현하고 JInt 라이브러리를 이용했습니다.

네이버에서 로그인할 때 이용하는 보안을 분석하고 우회하며
댓글을 작성할 때 네이버에서 정의한 bvsd를 분석하여 그대로 구현합니다.

## bvsd
네이버에서는 로그인할 때 네이버에서 정의한 bvsd를 필요로 합니다. (2017년에는 없었는데 2018년에 생긴것같음)
이것을 분석하여 그대로 구현하여 넘겨줘야만 정상적으로 로그인이 됩니다.
사실 2017년에 구현을 한번 했었는데 2018년에 다시 하려니까 안돼서 이유를 찾다가 발견했습니다. 네이버에서도 신경을 쓰고 있는 것 같음.

## 패킷 분석
로그인에 성공한 뒤에는 댓글이 등록, 수정, 삭제되는 패킷을 잘 캡처하여 같은 패킷을 그대로 전송해주기만 하면 됩니다.
https로 전송하기 때문에 패킷이 쉽지 않은데 네이버에서 사용하는 자바스크립트에 전송하는 URL을 찾을 수 있습니다. https로 되어 있는 것을 http로 바꿔서 js를 다시 에뮬레이팅해준 뒤에 캡처하면 데이터를 볼 수 있습니다.

## 개발 설명
* [네이버 로그인 보안 분석 - 1 (세션키)](https://blog.naver.com/boyism/221424546234)
* [네이버 로그인 보안 분석 - 2 (bvsd)](https://blog.naver.com/boyism/221424548358)
* [네이버 로그인 보안 분석 - 3(bvsd:uuid)](https://blog.naver.com/boyism/221424549546)
* [네이버 로그인 보안 분석 - 4(bvsd:keyboard)](https://blog.naver.com/boyism/221424550656)
* [네이버 로그인 보안 분석 - 5(bvsd:device)](https://blog.naver.com/boyism/221424551365)
* [네이버 로그인 보안 분석 - 6(bvsd:mouse)](https://blog.naver.com/boyism/221424552716)
* [네이버 로그인 보안 분석 - 7(bvsd:duration, hash, components)](https://blog.naver.com/boyism/221424553997)
* [네이버 로그인 보안 분석 - 8(bvsd 인코딩)](https://blog.naver.com/boyism/221424555109)
* [네이버 로그인 보안 분석 - 9(전송, 마무리)](https://blog.naver.com/boyism/221424556615)
* [네이버 뉴스 댓글 자동달기/삭제](https://blog.naver.com/boyism/221430121772)

## 실행결과
* [유튜브 영상 보기](https://youtu.be/As5ZQz000tg)
