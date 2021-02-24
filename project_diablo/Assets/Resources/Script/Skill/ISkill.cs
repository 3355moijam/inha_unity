using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 스킬 발동 버튼을 누르면 일어나야 하는 일
 * 1. 캐릭터의 애니메이션이 실행
 * 2. 스킬 이펙트가 발동
 * 3. 대미지 처리
 */

public interface ISkill
{
	//Object[] runes { get; set; }
	void OnButtonDown();
	void OnButton();
	void OnButtonUp();
	bool HasAnimation();
	void SetRune(int num);

	void Init();
	void Clear(); 
}

